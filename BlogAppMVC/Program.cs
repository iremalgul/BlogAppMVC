using BlogAppMVC.Context;
using BlogAppMVC.Services.Implements;
using BlogAppMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Sql_connection");
builder.Services.AddDbContext<BlogContext>(option =>
    option.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IUserService, UserService>(); 
builder.Services.AddTransient<ICommentService, CommentService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.AccessDeniedPath = "/User/AccessDenied";
    options.LoginPath = "/User/Login";
    options.LogoutPath = "/User/Logout";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


SeedData.PushData(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "post_details",
    pattern: "blogs/{url}",
    defaults: new { controller = "Post", action = "Details" }
);
app.MapControllerRoute(
    name: "posts_by_category",
    pattern: "blogs/category/{category}",
    defaults: new { controller = "Post", action = "Index" }
);
app.MapControllerRoute(
    name: "user_profile",
    pattern: "profile/{username}",
    defaults: new { controller = "User", action = "Profile" }
);


app.Run();
