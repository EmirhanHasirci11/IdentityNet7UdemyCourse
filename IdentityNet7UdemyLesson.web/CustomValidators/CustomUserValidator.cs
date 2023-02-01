using IdentityNet7UdemyLesson.web.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityNet7UdemyLesson.web.CustomValidators
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError> identityErrors= new List<IdentityError>();

            if (char.IsNumber(user.UserName[0]))
            {
                identityErrors.Add(new() { Code = "UserNameStartsWithNumber", Description = "Kullanıcı adı bir sayı ile başlayamaz" });
            }
            if (identityErrors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(identityErrors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
