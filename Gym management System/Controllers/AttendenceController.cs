using Gym_management_System.Models.Attendences;
using Gym_management_System.Models.Members;
using Microsoft.AspNetCore.Mvc;

namespace Gym_management_System.Controllers
{
    public class AttendenceController:BaseController
    {
        private readonly IAttendence attendenceService;
        public AttendenceController(IAttendence _attendenceService) 
        {
            attendenceService= _attendenceService;
        }
        public IActionResult index(int gymId)
        {
            ViewData["CurrendGymId"] = gymId;
            var model=attendenceService.GetTodaysAttendenceStatus(gymId);
            return View(model);
        }
        [HttpPost]
        public IActionResult MarkPresent(int memberId,int gymId)
        {
            attendenceService.MarkPresent(memberId);
            return RedirectToAction("index", new { gymId });

        }
        [HttpPost]
        public IActionResult MarkCheckOut(int memberId, int gymId)
        {
            attendenceService.MarkCheckOut(memberId);
            return RedirectToAction("index", new { gymId });

        }
    }
}
