using IdentityNet7UdemyLesson.web.Extensions;
using IdentityNet7UdemyLesson.web.Models;
using IdentityNet7UdemyLesson.web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace IdentityNet7UdemyLesson.web.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileProvider _fileProvider;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IFileProvider fileProvider)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _fileProvider = fileProvider;
        }
        public async Task<IActionResult> Index()
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var user = new UserViewModel();
            user.UserName = appUser.UserName;
            user.Email = appUser.Email;
            user.PhoneNumber = appUser.PhoneNumber;
            user.PictureUrl = appUser.Picture;
            return View(user);
        }
        [HttpGet]
        public IActionResult PasswordChange()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel p)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var checkTheOldPassword = await _userManager.CheckPasswordAsync(currentUser, p.OldPassword);
            if (!checkTheOldPassword)
            {
                ModelState.AddModelError("", "Lütfen eski şifrenizi doğru girdiğinizden emin olunuz");
                return View(p);
            }
            var result = await _userManager.ChangePasswordAsync(currentUser, p.OldPassword, p.Password);
            if (!result.Succeeded)
            {
                ModelStateExtension.AddModelErrorList(ModelState, result.Errors.Select(x => x.Description).ToList());
                return View(p);
            }
            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser, p.Password, true, false);

            return View();
        }
        [HttpGet]
        public IActionResult UserEdit()
        {
            ViewBag.gender = new SelectList(Enum.GetNames(typeof(Gender)));
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var userEdit = new UserEditViewModel()
            {
                Email = user.Email,
                UserName = user.UserName,
                Phone = user.PhoneNumber,
                BirthDate = user.BirthDate,
                City = user.City,
                Gender = user.Gender
            };
            return View(userEdit);
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditViewModel p)
        {
            ViewBag.gender = new SelectList(Enum.GetNames(typeof(Gender)));
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            currentUser.Email = p.Email;
            currentUser.UserName = p.UserName;
            currentUser.PhoneNumber = p.Phone;
            currentUser.BirthDate = p.BirthDate;
            currentUser.City = p.City;
            currentUser.Gender = p.Gender;
            if (p.Picture != null && p.Picture.Length > 0)
            {
                var wwwrootFolder = _fileProvider.GetDirectoryContents("wwwroot");
                string randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(p.Picture.FileName)}";
                var newPicturePath = Path.Combine(wwwrootFolder.First(x => x.Name == "userPictures").PhysicalPath, randomFileName);
                using var stream = new FileStream(newPicturePath, FileMode.Create);
                await p.Picture.CopyToAsync(stream);
                currentUser.Picture = randomFileName;

            }
           var result= await _userManager.UpdateAsync(currentUser);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(p);
            }
         
            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(currentUser, true);
            var userEdit = new UserEditViewModel()
            {
                Email = currentUser.Email,
                UserName = currentUser.UserName,
                Phone = currentUser.PhoneNumber,
                BirthDate = currentUser.BirthDate,
                City = currentUser.City,
                Gender = currentUser.Gender
            };
            return View(userEdit);
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
