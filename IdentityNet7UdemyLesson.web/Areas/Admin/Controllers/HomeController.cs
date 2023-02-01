using IdentityNet7UdemyLesson.web.Areas.Admin.Models;
using IdentityNet7UdemyLesson.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityNet7UdemyLesson.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        public HomeController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UserList()
        {
            var userList = await userManager.Users.ToListAsync();
            var userViewModelList = userList.Select(x => new UserViewModel()
            {
                UserID = x.Id,
                UserName = x.UserName,
                Email = x.Email
            });
            return View(userViewModelList);
        }
    }
}
