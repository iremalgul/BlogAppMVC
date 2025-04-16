using BlogAppMVC.DTOs;
using BlogAppMVC.Models;

namespace BlogAppMVC.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<PostDTO>> GetAll();
        Task<PostDTO> GetPost(string url);
        Task<PostDTO> GetPostById(int id);
        Task<int> CreatePost(PostDTO model);
        Task<int> UpdatePost(PostDTO model, bool isAdmin);
        Task<bool> DeletePost(int postId);
        Task<(List<PostDTO> Posts, int TotalCount)> GetPostsAsync(string category, int page, int pageSize);
        Task<List<PostDTO>> GetPostsByUserId(int userId);
     
    }
}
