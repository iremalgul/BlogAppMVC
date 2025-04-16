using BlogAppMVC.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogAppMVC.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAll();
        Task<List<CategoryDTO>> GetByIds(List<int> ids);
        Task<List<SelectListItem>> GetCategorySelectList();
    }
}
