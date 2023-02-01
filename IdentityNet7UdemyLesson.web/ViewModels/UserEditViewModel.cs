using IdentityNet7UdemyLesson.web.Models;
using System.ComponentModel.DataAnnotations;

namespace IdentityNet7UdemyLesson.web.ViewModels
{
    public class UserEditViewModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        public string UserName { get; set; }
        [Display(Name = "Mail adresi")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen geçerli bir mail formatı girdiğinizden emin olun")]
        [Required(ErrorMessage = "Mail zorunludur")]
        public string Email { get; set; }
        [Display(Name = "Telefon numarası")]
        [Required(ErrorMessage = "Telefon zorunludur")]
        public string Phone { get; set; }
        [Display(Name = "Şehir")]

        public string City { get; set; }
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Resim")]
        public IFormFile Picture { get; set; }
        [Display(Name = "Cinsiyet")]
        public Gender Gender { get; set; }
    }
}
