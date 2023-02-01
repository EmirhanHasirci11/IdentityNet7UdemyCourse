using System.ComponentModel.DataAnnotations;

namespace IdentityNet7UdemyLesson.web.ViewModels
{
    public class SignUpViewModel
    {
        [Display(Name ="Kullanıcı Adı")]
        [Required(ErrorMessage ="Kullanıcı adı zorunludur")]
        public string UserName{ get; set; }
        [Display(Name ="Mail adresi")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen geçerli bir mail formatı girdiğinizden emin olun")]
        [Required(ErrorMessage ="Mail zorunludur")]
        public string Email{ get; set; }
        [Display(Name ="Telefon numarası")]
        [Required(ErrorMessage ="Telefon zorunludur")]
        public string Phone{ get; set; }
        [Required(ErrorMessage ="Şifre zorunludur")]
        [Display(Name ="Şifre")]
        [DataType(DataType.Password)]
        public string Password{ get; set; }
        [Required(ErrorMessage ="Şifrenizi yeniden girmeniz zorunludur")]
        [Display(Name ="Şifre Yeniden")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Şifreler birbiriyle eşleşmiyor")]
        public string ConfirmPassword{ get; set; }
    }
}
