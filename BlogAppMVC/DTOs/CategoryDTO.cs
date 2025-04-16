using BlogAppMVC.Context.Entities;

namespace BlogAppMVC.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string? Text { get; set; }
        public string? Url { get; set; }
        public CategoryColors? Color { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
