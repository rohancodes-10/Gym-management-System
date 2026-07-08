using Gym_management_System.Models.Complaints;
using Gym_management_System.ViewModels.ComplaintsViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gym_management_System.Controllers
{
    public class ComplaintController:BaseController
    {
        private readonly IComplaintService _complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService=complaintService;
        }
        //public IActionResult Index(int gymId)
        //{
        //    if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        //    if (!IsMember() && !IsTrainer()) return RedirectToAction("Login", "Account");

        //    var complaints = _complaintService.GetAllComplaintByGymId(gymId);
        //    ViewData["CurrentGymId"] = gymId;

        //    var model = new CreateComplaintsViewModel
        //    {
        //       GymId=gymId,
        //       complaints=complaints,
        //    };
        //    return View(model);
        //}

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsMember() && !IsTrainer()) return RedirectToAction("Login", "Account");
            
            var gymid= GetGymId() ?? 0;
            ViewData["CurrentGymId"] = gymid;
            CreateComplaintsViewModel model = new CreateComplaintsViewModel
            {
                GymId =gymid
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateComplaintsViewModel model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsMember() && !IsTrainer()) return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                ViewData["CurrentGymId"] = model.GymId;
                return View(model);
            }

            var complaint = new Complaint
            {
                GymId = model.GymId,
                SubmittedById = GetUserId() ?? 0,
                SubmittedByName=GetUserName()?? "Unknown",
                SubmittedByRole=GetRole()??"Unknown",
                Subject=model.Subject,
                Message=model.Message
            };
            _complaintService.AddComplaint(complaint);
            TempData["Success"] = "Your complaint has been submitted.";

            return RedirectToAction("MyComplaints");
        }

    }
}
