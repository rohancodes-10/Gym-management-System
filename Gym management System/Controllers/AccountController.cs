using Gym_management_System.Models;
using Gym_management_System.Models.Users;
using Gym_management_System.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gym_management_System.Controllers
{
    public class AccountController : Controller
    {
        private IAuthService _authSevice;
        public AccountController(IAuthService authService)
        {
            _authSevice=authService;
        }
        [HttpGet]
       public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
    }
}
