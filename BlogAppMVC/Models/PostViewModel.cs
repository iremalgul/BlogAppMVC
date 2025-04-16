using BlogAppMVC.DTOs;

namespace BlogAppMVC.Models
{
    public class PostViewModel
    {
        public List<PostDTO> Posts { get; set; } = new();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
