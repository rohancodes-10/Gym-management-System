using Gym_management_System.Models;
using Gym_management_System.Models.Users;
using Gym_management_System.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gym_management_System.Controllers
{
    public class AccountController : Controller
    {
        private IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService=authService;
        }
        [HttpGet]
       public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _authService.Login(model.Email, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "invalid Email or password");
                return View(model);
            }
            HttpContext.Session.SetInt32("userId", user.Id);
            HttpContext.Session.SetString("userRole", user.Role);
            HttpContext.Session.SetString("userName", user.Name);

            if (user.GymId.HasValue)
            {
                HttpContext.Session.SetInt32("GymId", user.GymId.Value);
            }
            return RedirectToAction("index","Gym");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Role = "Owner", 
                GymId = null,
                RoleId = null
            };

            _authService.Register(user, model.Password);
            return RedirectToAction("Login");
        }
    }
}
