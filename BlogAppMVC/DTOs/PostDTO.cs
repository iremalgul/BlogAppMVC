using BlogAppMVC.Context.Entities;

namespace BlogAppMVC.DTOs
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? Image { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
}
