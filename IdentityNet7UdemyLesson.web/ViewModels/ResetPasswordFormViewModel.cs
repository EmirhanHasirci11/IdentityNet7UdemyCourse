using System.ComponentModel.DataAnnotations;

namespace IdentityNet7UdemyLesson.web.ViewModels
{
    public class ResetPasswordFormViewModel
    {
        [Required(ErrorMessage = "Şifre zorunludur")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Şifrenizi yeniden girmeniz zorunludur")]
        [Display(Name = "Şifre Yeniden")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler birbiriyle eşleşmiyor")]
        public string ConfirmPassword { get; set; }
    }
}
