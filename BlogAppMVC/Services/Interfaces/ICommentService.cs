using BlogAppMVC.DTOs;

namespace BlogAppMVC.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDTO> CreateComment(CommentDTO dto);
        Task<bool> DeleteComment(int commentId);
    }
}
