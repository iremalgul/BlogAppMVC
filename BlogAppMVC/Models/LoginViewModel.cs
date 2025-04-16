using System.ComponentModel.DataAnnotations;

namespace BlogAppMVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Eposta alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir eposta adresi giriniz.")]
        [Display(Name = "Eposta")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Parola alanı boş bırakılamaz.")]
        [StringLength(10, ErrorMessage = "{0} alanı en az {2} ve en fazla {1} karakter olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string? Password { get; set; }
    }
}
