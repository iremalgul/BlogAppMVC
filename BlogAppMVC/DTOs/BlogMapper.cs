using AutoMapper;
using BlogAppMVC.Context.Entities;
using BlogAppMVC.Models;

namespace BlogAppMVC.DTOs
{
    public class BlogMapper : Profile
    {
        public BlogMapper() {

            CreateMap<Post, PostDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Category  , CategoryDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Post, PostCreateViewModel>().ReverseMap();
            

        }
    }
}
