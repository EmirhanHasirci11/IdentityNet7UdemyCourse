using IdentityNet7UdemyLesson.web.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityNet7UdemyLesson.web.CustomValidators
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> identityErrors= new List<IdentityError>();
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                identityErrors.Add(new IdentityError() { Description = "Şifre kullanıcı adını içeremez", Code = "PasswordContainsUserName" });
            }
            if (identityErrors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(identityErrors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
