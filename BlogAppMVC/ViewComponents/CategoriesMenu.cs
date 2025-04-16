using AutoMapper;
using BlogAppMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace BlogAppMVC.ViewComponents
{

    public class CategoriesMenu: ViewComponent{

        private readonly ICategoryService _categoryService;
        private IMapper Mapper
        {
            get;
        }


        public CategoriesMenu(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            Mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(){
            var categories = await _categoryService.GetAll(); 
            return View(categories);
        }
    }
}