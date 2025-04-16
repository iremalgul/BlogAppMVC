using System.ComponentModel.DataAnnotations;

namespace BlogAppMVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı alanı boş bırakılamaz.")]
        [Display(Name = "Kullanıcı Adı")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Ad-Soyad alanı boş bırakılamaz.")]
        [Display(Name = "Ad Soyad")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Eposta alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir eposta adresi giriniz.")]
        [Display(Name = "Eposta")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Parola alanı boş bırakılamaz.")]
        [StringLength(10, ErrorMessage = "{0} alanı en az {2} ve en fazla {1} karakter olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Parola tekrar alanı boş bırakılamaz.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Parolanız eşleşmiyor.")]
        [Display(Name = "Parola Tekrar")]
        public string? ConfirmPassword { get; set; }

        [Display(Name = "Profil Fotoğrafı")]
        public IFormFile? ImageFile { get; set; }
    }
}
