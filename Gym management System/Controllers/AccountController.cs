using Gym_management_System.Models;
using Gym_management_System.Models.Users;
using Gym_management_System.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gym_management_System.Controllers
{
    public class AccountController : BaseController
    {
        private IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService=authService;
        }
        [HttpGet]
       public IActionResult Login()
        {
            //if (IsLoggedIn())
            //{ return RedirectToAction("index", "Gym"); }
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Error: {error.ErrorMessage}");
            }
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
            HttpContext.Session.SetInt32("RoleId", user.RoleId??0);

            if (user.GymId.HasValue)
            {
                HttpContext.Session.SetInt32("GymId", user.GymId.Value);
            }
            return user.Role switch
            {
                "Owner" => RedirectToAction("index", "Gym"),
                "Manager" => RedirectToAction("Details", "Gym", new { id = user.GymId }),
                "Trainer" => RedirectToAction("Details", "Trainer", new { id = user.RoleId }),
                "Member" => RedirectToAction("Details", "Member", new { id = user.RoleId }),
                _ => RedirectToAction("Login", "Account")
            };
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = _authService.GetUserByEmail_username(model.Email, model.UserName);
            if (user == null) 
            {
                ModelState.AddModelError("","wrong email and username");
                return View(model);
            }
            return RedirectToAction("ResetPassword", new { userId = user.Id });
        }
        [HttpGet]
        public IActionResult ResetPassword(int userId)
        {
            var user=_authService.GetUserById(userId);
            if (user == null) return NotFound();

            var model = new ResetPasswordViewModel
            {
                UserId=user.Id,
                UserName=user.Name
            };


            return View(model);
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model )
        {
            var user = _authService.GetUserById(model.UserId);
            if (user == null) return NotFound();
            if (!ModelState.IsValid)
            {
                model.UserName = user.Name; 
                return View(model);
            }

            _authService.ResetPassword(model.UserId, model.NewPassword);
            return RedirectToAction("Login", "Account");
        }
        public  IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
