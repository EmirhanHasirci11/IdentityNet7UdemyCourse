using IdentityNet7UdemyLesson.web.Extensions;
using IdentityNet7UdemyLesson.web.Models;
using IdentityNet7UdemyLesson.web.Services;
using IdentityNet7UdemyLesson.web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IdentityNet7UdemyLesson.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignIn(string? ReturnUrl = null)
        {
            TempData["returnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(SignInViewModel p)
        {
            string ReturnUrl = TempData["returnUrl"].ToString();
            ReturnUrl ??= Url.Action("Index", "Home");
            if (ModelState.IsValid)
            {
                AppUser user = _userManager.FindByEmailAsync(p.Email).Result;
                if (user != null)
                {
                    var result = _signInManager.PasswordSignInAsync(user, p.Password, p.RememberMe, true).Result;
                    if (result.Succeeded)
                    {
                        _userManager.ResetAccessFailedCountAsync(user);
                        return Redirect(ReturnUrl);
                    }
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "3 Dakika boyunca sisteme giriş yapamazsınız");
                        return View(p);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Hatalı email veya şifre");
                }
            }
            return View(p);
        }


        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {

                AppUser user = new AppUser();
                user.UserName = signUpViewModel.UserName;
                user.PhoneNumber = signUpViewModel.Phone;
                user.Email = signUpViewModel.Email;

                IdentityResult result = await _userManager.CreateAsync(user, signUpViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelStateExtension.AddModelErrorList(ModelState, result.Errors.Select(X => X.Description).ToList());
                }
            }
            return View(signUpViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ResetPasswordViewModel p)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(p.Email);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Bu maile sahip bir kullanıcı bulunamadı");
                return View(p);
            }
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);

            var passwordResetLink = Url.Action("ResetPassword", "Home", new {id=appUser.Id,token=passwordResetToken},HttpContext.Request.Scheme);
            await _emailService.SendResetPasswordEmail(passwordResetLink, p.Email);
            return RedirectToAction(nameof(ForgetPassword));


        }
        [HttpGet]
        public IActionResult ResetPassword(string id,string token)
        {
            TempData["id"] = id;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordFormViewModel p)
        {
            var userId = TempData.Peek("id").ToString();
            var token= TempData.Peek("token").ToString();
            var user = _userManager.FindByIdAsync(userId).Result;
            
            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı bulunamamıştır");
                return View();
            }

            var result = _userManager.ResetPasswordAsync(user, token, p.Password).Result;
            if(result.Succeeded) 
            {
                return RedirectToAction("SignIn","Home");
            }
            else
            {
                ModelStateExtension.AddModelErrorList(ModelState,result.Errors.Select(x => x.Description).ToList());
            }
            return View(p);
        }
    }
}