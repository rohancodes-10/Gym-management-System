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
        //[HttpPost]
        //public IActionResult Create(CreateComplaintsViewModel model)
        //{
        //    if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        //    if (!IsMember() && !IsTrainer()) return RedirectToAction("Login", "Account");

        //    if (!ModelState.IsValid)
        //    {
        //        ViewData["CurrentGymId"] = model.GymId;
        //        return View(model);
        //    }

        //   var complaint=new Complaint
        //   {
        //       GymId=ModelBinderAttribute.
        //   }
        //    CreateComplaintsViewModel model = new CreateComplaintsViewModel
        //    {
        //        GymId = gymid
        //    };
        //    return View(model);
        //}

    }
}
