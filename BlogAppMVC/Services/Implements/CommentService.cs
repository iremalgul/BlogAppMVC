using AutoMapper;
using BlogAppMVC.Context.Entities;
using BlogAppMVC.DTOs;
using BlogAppMVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.UnitOfWork;

namespace BlogAppMVC.Services.Implements
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        private IMapper Mapper
        {
            get;
        }

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<CommentDTO> CreateComment(CommentDTO dto)
        {
            var entity = Mapper.Map<Comment>(dto); // DTO → Entity dönüşümü

            await _unitOfWork.Comment.Insert(entity);

            return Mapper.Map<CommentDTO>(entity); // Entity → DTO dönüşümü
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            try
            {
                var comment = await _unitOfWork.Comment.Table.FirstOrDefaultAsync(c => c.CommentId == commentId);
                if (comment == null)
                    return false;

                await _unitOfWork.Comment.Delete(comment);

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
