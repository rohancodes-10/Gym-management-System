using Microsoft.AspNetCore.Mvc;
using Gym_management_System.Models.Staffs;
using Gym_management_System.ViewModels.StaffViewModels;

namespace Gym_management_System.Controllers
{
    public class StaffController:Controller
    {
        private readonly IStaffService staffService;
        public StaffController(IStaffService staffService)
        {
            this.staffService = staffService;
        }
        public IActionResult Index(int gymId)
        {
            var staff=staffService.GetAllStaffByGymId(gymId);
            var model = new StaffHomeViewModel
            {
                staffs = staff,
                gymId = gymId
            };
            return View(model);
        }
    }
}
