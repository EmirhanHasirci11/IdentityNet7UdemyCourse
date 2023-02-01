using IdentityNet7UdemyLesson.web.CustomValidators;
using IdentityNet7UdemyLesson.web.Localizations;
using IdentityNet7UdemyLesson.web.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityNet7UdemyLesson.web.Extensions
{
    public static class StartupExtension
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(opts =>
             {
                 opts.User.RequireUniqueEmail = true;
                 opts.Password.RequireDigit = false;
                 opts.Password.RequireNonAlphanumeric = false;
                 opts.Password.RequireUppercase = false;
                 opts.Password.RequiredLength = 4;
                 opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                 opts.Lockout.MaxFailedAccessAttempts = 3;
             }).AddEntityFrameworkStores<AppDbContext>()
               .AddPasswordValidator<CustomPasswordValidator>()
               .AddUserValidator<CustomUserValidator>()
               .AddDefaultTokenProviders()
               .AddErrorDescriber<LocalizationIdentityErrorDescriber>();
        }
    }
}
