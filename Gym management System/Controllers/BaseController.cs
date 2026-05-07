using Microsoft.AspNetCore.Mvc;

namespace Gym_management_System.Controllers
{
    public class BaseController : Controller
    {
        protected string? GetRole() =>
              HttpContext.Session.GetString("userRole");
        protected int? GetUserId() =>
            HttpContext.Session.GetInt32("userId");
        protected int? GetGymId() =>
            HttpContext.Session.GetInt32("GymId");
        protected string? GetUserName() =>
            HttpContext.Session.GetString("userName");
        protected int? GetRoleId() => 
            HttpContext.Session.GetInt32("RoleId");
        protected bool IsLoggedIn() => GetRole() != null;
        protected bool IsOwner() => GetRole() == "Owner";
        protected bool IsManager() => GetRole() == "Manager";
        protected bool IsTrainer() => GetRole() == "Trainer";
        protected bool IsMember() => GetRole() == "Member";

    }
}
