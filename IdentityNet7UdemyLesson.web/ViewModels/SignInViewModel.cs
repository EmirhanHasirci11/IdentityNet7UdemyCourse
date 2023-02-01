using System.ComponentModel.DataAnnotations;

namespace IdentityNet7UdemyLesson.web.ViewModels
{
    public class SignInViewModel
    {
        [Display(Name ="Mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Mail Zorunludur")]
        public string Email{ get; set; }
        [Display(Name ="Şifre")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Şifre Zorunludur")]
        [MinLength(4,ErrorMessage ="Şifre en az 4 basamaktan oluşmalıdır")]
        public string Password{ get; set; }
        public bool RememberMe{ get; set; }
    }
}
