using AutoMapper;
using BlogAppMVC.DTOs;
using BlogAppMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace BlogAppMVC.ViewComponents{

    public class NewPosts: ViewComponent{
        private readonly IPostService _postService;
        private IMapper Mapper
        {
            get;
        }


        public NewPosts(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            Mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(){
            var posts = await _postService.GetAll();
            posts = posts.Where(p => p.IsActive).ToList();

            // AutoMapper ile dönüþüm
            var postDtos = Mapper.Map<List<PostDTO>>(posts
                .OrderByDescending(p => p.PublishedOn)
                .Take(5)
                .ToList());

            return View(postDtos);  // DTO'yu View'a gönder
        }
    }
}