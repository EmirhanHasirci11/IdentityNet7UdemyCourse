using System.ComponentModel.DataAnnotations;

namespace IdentityNet7UdemyLesson.web.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "Mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Mail Zorunludur")]
        public string Email{ get; set; }
    }
}
