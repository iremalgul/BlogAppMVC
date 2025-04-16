using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogAppMVC.Context.Entities;
using BlogAppMVC.DTOs;
using BlogAppMVC.Models;
using BlogAppMVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.UnitOfWork;
using System;

namespace BlogAppMVC.Services.Implements
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;

        private IMapper Mapper
        {
            get;
        }

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, ICategoryService categoryService)
        {
            _unitOfWork = unitOfWork;
            Mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<List<PostDTO>> GetAll()
        {
            var posts = await _unitOfWork.Post.Table
                        .Include(p => p.User) // User bilgilerini de dahil et
                        .ToListAsync();

            return Mapper.Map<List<PostDTO>>(posts);
        }

        public async Task<(List<PostDTO> Posts, int TotalCount)> GetPostsAsync(string category, int page, int pageSize)
        {
            var query = _unitOfWork.Post.Table.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.Categories.Any(c => c.Url == category));
            }

            var totalCount = await query.CountAsync();

            var posts = await query
                .Include(p => p.User)
                .Where(p => p.IsActive)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // AutoMapper ile Post nesnelerini PostDTO'ya map ediyoruz
            var postDTOs = Mapper.Map<List<PostDTO>>(posts);

            return (postDTOs, totalCount);  
        }


        public async Task<PostDTO> GetPost(string url)
        {
            var post = await _unitOfWork.Post.Table
                .Include(x => x.User)
                .Include(x => x.Categories)
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(p => p.Url == url);

            // Post null ise, geri dönülecek bir şey olmayacak.
            if (post == null)
            {
                return null;
            }

            // AutoMapper ile dönüşüm
            return Mapper.Map<PostDTO>(post);
        }
        public async Task<List<PostDTO>> GetPostsByUserId(int userId)
        {
            var posts=  await _unitOfWork.Post.Table
                .Where(p => p.UserId == userId)
                .Include(p => p.User)
                .ToListAsync();

            return Mapper.Map<List<PostDTO>>(posts);
        }


        public async Task<PostDTO> GetPostById(int id)
        {
            var post = _unitOfWork.Post.Table.Include(p => p.Categories).FirstOrDefault(i => i.PostId == id);

            if (post == null)
            {
                return null;
            }

            // AutoMapper ile dönüşüm
            return Mapper.Map<PostDTO>(post);
        }

        public async Task<int> CreatePost(PostDTO model)
        {
            try
            {
                var post = Mapper.Map<Post>(model);

                // Kategori ID’lerini al
                var categoryIds = model.Categories.Select(c => c.CategoryId).ToList();

                // Gerçek Category entity’lerini veritabanından çek
                var categories = await _unitOfWork.Category.Table
                    .Where(c => categoryIds.Contains(c.CategoryId))
                    .ToListAsync();

                // Entity'leri ilişkilendir
                post.Categories = categories;

                await _unitOfWork.Post.Insert(post);
                return post.PostId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<int> UpdatePost(PostDTO model, bool isAdmin)
        {
            try
            {
                var post = await _unitOfWork.Post.Table.Include(p => p.Categories).FirstOrDefaultAsync(p => p.PostId == model.PostId);

                if (post == null)
                    return -1;

                Mapper.Map(model, post); 

                if (isAdmin)
                {
                    post.IsActive = model.IsActive;
                }

                // Yeni kategorileri eşle
                if (model.Categories != null && model.Categories.Any())
                {
                    var categoryIds = model.Categories.Select(c => c.CategoryId).ToList();

                    var categories = await _unitOfWork.Category.Table
                        .Where(c => categoryIds.Contains(c.CategoryId))
                        .ToListAsync();

                    post.Categories = categories;
                }

                await _unitOfWork.Post.Update(post);
               

                return post.PostId;
            }
            catch (Exception)
            {
                return -1;
            }
        }




        public async Task<bool> DeletePost(int postId)
        {
            try
            {
                var post = await _unitOfWork.Post
                          .Table
                          .Include(p => p.Categories) // ilişkili kategorileri çek
                          .FirstOrDefaultAsync(p => p.PostId == postId);

                if (post == null)
                    return false;

                // ilişkileri temizle (bu CategoryPost tablosundaki kayıtları siler)
                post.Categories.Clear();

                await _unitOfWork.Post.Delete(post);
                

                return true;
            }
            catch
            {
                return false;
            }
        }

       

    }
}
