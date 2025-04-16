using AutoMapper;
using BlogAppMVC.Context.Entities;
using BlogAppMVC.DTOs;
using BlogAppMVC.Helpers;
using BlogAppMVC.Models;
using BlogAppMVC.Services.Implements;
using BlogAppMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BlogAppMVC.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly ICategoryService _categoryService;

        public PostController(IPostService postService, ICommentService commentService, ICategoryService categoryService)
        {
            _postService = postService;
            _commentService = commentService;   
            _categoryService = categoryService;
        }


        public async Task<IActionResult> Index(string category, int page = 1, int pageSize = 4)
        {
            // Service'den veriyi alıyoruz
            var result = await _postService.GetPostsAsync(category, page, pageSize);

            var posts = result.Posts;  // List<PostDTO> posts
            var totalPosts = result.TotalCount;  // int totalPosts

            var totalPages = (int)Math.Ceiling((double)totalPosts / pageSize);

            var viewModel = new PostViewModel
            {
                Posts = posts, // Burada PostDTO'ları kullanıyoruz
                CurrentPage = page,
                TotalPages = totalPages
            };

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_PostListPartial", viewModel); // AJAX için partial view
            }

            return View(viewModel);
        }




        public async Task<IActionResult> Details(string url)
        {
            var post = await _postService.GetPost(url);
            return View(post);
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            // Kullanıcı bilgilerini al
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(); // Kullanıcı kimliği yoksa erişimi engelle
            }

            int userId = int.Parse(userIdClaim);

            // Eğer rol yoksa, sadece kullanıcıya ait postları getir
            var posts = string.IsNullOrEmpty(role)
                ? await _postService.GetPostsByUserId(userId)
                : await _postService.GetAll(); // Adminse tüm postları getir

            return View(posts);
        }


        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = new PostCreateViewModel
            {
                CategoryList = await _categoryService.GetCategorySelectList()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PostCreateViewModel model)
        {
            // Görsel doğrulaması
            if (model.Image == null || model.Image.Length == 0)
            {
                ModelState.AddModelError("Image", "Resim yüklemek zorunludur.");
            }
            // Kategori doğrulaması
            if (model.SelectedCategoryIds == null || !model.SelectedCategoryIds.Any())
            {
                ModelState.AddModelError("SelectedCategoryIds", "En az bir kategori seçilmelidir.");
            }
            // Eğer model geçerli değilse, kategori listesini yeniden yükleyip kullanıcıya formu göster
            if (!ModelState.IsValid)
            {
                model.CategoryList = await _categoryService.GetCategorySelectList();
                return View(model);
            }

            // Görseli kaydetme
            var fileName = await ImageHelper.SaveImageAsync(model.Image);

            // Kullanıcı ID'sini al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);

            // Seçilen kategorileri alma
            var selectedCategories = await _categoryService.GetByIds(model.SelectedCategoryIds);

            // Post DTO oluşturma
            var dto = new PostDTO
            {
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                Url = model.Url,
                Image = fileName,
                IsActive = false,
                PublishedOn = DateTime.Now,
                UserId = userId,
                Categories = selectedCategories
            };

            // Post oluşturma işlemi
            var result = await _postService.CreatePost(dto);

            // Başarılı işlem sonrası yönlendirme
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Yazı başarıyla oluşturuldu.";
                return RedirectToAction("List");
            }

            // Eğer işlem başarısızsa, hata mesajı ekleyip kategori listesiyle formu geri gönder
            TempData["ErrorMessage"] = "Yazı oluşturulurken bir hata oluştu.";
            ModelState.AddModelError("", "Yazı oluşturulurken bir hata oluştu.");
            model.CategoryList = await _categoryService.GetCategorySelectList();
            return View(model);
        }


        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            // PostDTO'yu almak
            var postDTO = await _postService.GetPostById(id);

            if (postDTO == null)
            {
                return NotFound();
            }

            // PostDTO'dan PostCreateViewModel'e dönüşüm
            var model = new PostCreateViewModel
            {
                PostId = postDTO.PostId,
                Title = postDTO.Title,
                Description = postDTO.Description,
                Content = postDTO.Content,
                Url = postDTO.Url,
                IsActive = postDTO.IsActive,
                ImageFileName = postDTO.Image,
                SelectedCategoryIds = postDTO.Categories.Select(c => c.CategoryId).ToList(),
                CategoryList = (await _categoryService.GetAll()).Select(c => new SelectListItem
                {
                    Text = c.Text,
                    Value = c.CategoryId.ToString(),
                    Selected = postDTO.Categories.Any(pc => pc.CategoryId == c.CategoryId)
                }).ToList()
            };

            // View'a gönderiyoruz
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(PostCreateViewModel model)
        {
            // Mevcut postu al
            var existingPost = await _postService.GetPostById(model.PostId);
            if (existingPost == null)
            {
                return NotFound();  
            }

            // Kategori doğrulaması
            if (model.SelectedCategoryIds == null || !model.SelectedCategoryIds.Any())
            {
                ModelState.AddModelError("SelectedCategoryIds", "En az bir kategori seçilmelidir.");
            }

            // Eğer model geçerli değilse, kategorilerle formu geri gönder
            if (!ModelState.IsValid)
            {
                model.ImageFileName = existingPost.Image;  // Mevcut görseli formda tut
                model.CategoryList = await _categoryService.GetCategorySelectList();
                return View(model);
            }

            // Admin kontrolü
            var isAdmin = User.FindFirstValue(ClaimTypes.Role) == "admin";

            // Yeni görsel kontrolü ve kaydetme
            if (model.Image != null && model.Image.Length > 0)
            {
                model.ImageFileName = await ImageHelper.SaveImageAsync(model.Image);
            }
            else
            {
                model.ImageFileName = existingPost.Image;  // Görsel yüklenmediyse mevcut görseli kullan
            }

            // Seçilen kategorileri al
            var selectedCategories = await _categoryService.GetByIds(model.SelectedCategoryIds);

            // Kullanıcı ID'sini al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();  
            }

            int userId = int.Parse(userIdClaim.Value);

            // DTO oluştur
            var dto = new PostDTO
            {
                UserId = userId,
                PostId = model.PostId,
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                Url = model.Url,
                Image = model.ImageFileName,
                IsActive = model.IsActive,
                Categories = selectedCategories
            };

            // Postu güncelle
            var result = await _postService.UpdatePost(dto, isAdmin);

            // Güncelleme başarılıysa listeye yönlendir
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Yazı başarıyla güncellendi.";
                return RedirectToAction("List");
            }

            // Hata oluştuysa modelde hata mesajı ekle ve formu geri gönder
            TempData["ErrorMessage"] = "Yazı güncellenirken bir hata oluştu.";
            ModelState.AddModelError("", "Post güncellenirken bir hata oluştu.");
            return View(model);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, message = "Geçersiz ID." });
            }

            var result = await _postService.DeletePost(id);

            if (!result)
            {
                return NotFound(new { success = false, message = "Yazı bulunamadı veya silinemedi." });
            }

            return Ok(new { success = true, message = "Yazı başarıyla silindi." });
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> AddComment(int PostId, string Text)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);



            var dto = new CommentDTO
            {
                PostId = PostId,
                Text = Text,
                UserId = int.Parse(userId ?? ""),
                PublishedOn = DateTime.Now
            };

            var createdComment = await _commentService.CreateComment(dto);

            var jsonResponse = new
            {
                username,
                createdComment.Text,
                createdComment.PublishedOn,
                avatar,
                success = true,
                message = "Yorum başarıyla eklendi.",
            };

            return Json(jsonResponse);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, message = "Geçersiz ID." });
            }

            var result = await _commentService.DeleteComment(id);

            if (!result)
            {
                return NotFound(new { success = false, message = "Yorum bulunamadı veya silinemedi." });
            }

            return Ok(new { success = true, message = "Yorum başarıyla silindi." });
        }

    }
}
