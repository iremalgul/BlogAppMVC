using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogAppMVC.DTOs;
using BlogAppMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.UnitOfWork;

namespace BlogAppMVC.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        private IMapper Mapper
        {
            get;
        }

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            var query = _unitOfWork.Category.Table;
            var categories = await query.ProjectTo<CategoryDTO>(Mapper.ConfigurationProvider).ToListAsync();
            return categories;
        }
        public async Task<List<CategoryDTO>> GetByIds(List<int> ids)
        {
            var query = _unitOfWork.Category.Table
                .Where(c => ids.Contains(c.CategoryId))
                .AsNoTracking();

            var selectedCategories = await query
                .ProjectTo<CategoryDTO>(Mapper.ConfigurationProvider)
                .ToListAsync();

            return selectedCategories;
        }

        public async Task<List<SelectListItem>> GetCategorySelectList()
        {
            var categories = await GetAll(); // Burada GetAll(), kategorileri alır.
            return categories.Select(c => new SelectListItem
            {
                Text = c.Text,
                Value = c.CategoryId.ToString()
            }).ToList();
        }
    }
}
