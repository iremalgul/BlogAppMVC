using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlogAppMVC.Models;
using BlogAppMVC.Services.Interfaces;
using BlogAppMVC.DTOs;
using BlogAppMVC.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace BlogAppMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Post");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUser(model);

                if (user != null)
                {
                    var userClaims = new List<Claim>();

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, user.UserName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, user.Name ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.UserData, user.Image ?? ""));

                    if (user.Email == "admin@example.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authPoperties = new AuthenticationProperties { IsPersistent = true };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authPoperties
                    );

                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                }
            }
            return View(model);
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userService.GetUser(new LoginViewModel
                {
                    Email = model.Email,
                    Password = model.Password
                });

                if (existingUser == null)
                {
                    string fileName = "default.jpg";

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        fileName = await ImageHelper.SaveImageAsync(model.ImageFile);
                    }

                    var newUser = new UserDTO
                    {
                        UserName = model.Username,
                        Name = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        Image = fileName
                    };

                    var createdUserId = await _userService.CreateUser(newUser);

                    if (createdUserId > 0)
                    {
                        return RedirectToAction("Login");
                    }

                    ModelState.AddModelError("", "Kullanıcı oluşturulurken bir hata oluştu.");
                }
                else
                {
                    ModelState.AddModelError("", "Eposta ya da kullanıcı adı kullanımda.");
                }
            }

            return View(model);
        }




        [Authorize]
        public async Task<IActionResult>  Profile(string username)
        {
            if (string.IsNullOrEmpty(username))
                return NotFound();

            var user = await _userService.GetUserProfile(username);

            if (user == null)
                return NotFound();

            return View(user);
        }

    }
}
