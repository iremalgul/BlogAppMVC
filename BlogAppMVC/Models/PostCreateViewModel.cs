using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlogAppMVC.Models
{
    public class PostCreateViewModel
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        [Display(Name = "Başlık")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Açıklama alanı zorunludur.")]
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "İçerik alanı zorunludur.")]
        [Display(Name = "İçerik")]
        public string? Content { get; set; }

        [Required(ErrorMessage = "URL alanı zorunludur.")]
        [Display(Name = "Url")]
        public string? Url { get; set; }
        
        [Display(Name = "Resim")]
        public IFormFile? Image { get; set; }
        public string? ImageFileName { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "En az bir kategori seçilmelidir.")]
        [Display(Name = "Kategoriler")]
        public List<int> SelectedCategoryIds { get; set; } = new(); // Seçili kategoriler
        public List<SelectListItem> CategoryList { get; set; } = new(); // Dropdown için
    }
}
