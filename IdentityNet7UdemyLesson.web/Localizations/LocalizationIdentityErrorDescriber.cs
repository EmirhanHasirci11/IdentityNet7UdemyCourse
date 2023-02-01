using Microsoft.AspNetCore.Identity;

namespace IdentityNet7UdemyLesson.web.Localizations
{
    public class LocalizationIdentityErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code = "PasswordTooShort", Description = $"Şifre en az {length} karakterden oluşmalıdır" };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "DuplicateEmail", Description = $"Bu {email} daha önce  başka bir kullanıcı tarafından oluşturuldu" };
        }
        public override IdentityError PasswordRequiresLower()
        {
            return new() {Code="PasswordRequiresLower",Description="Şifre en az bir adet küçük harf içermelidir." };
        }
    }
}
