using BlogAppMVC.Context.Entities;

namespace BlogAppMVC.DTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string? Text { get; set; }
        public DateTime PublishedOn { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
