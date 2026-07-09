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
        public IActionResult Index(int gymId)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsOwner() && !IsManager()) return RedirectToAction("Login", "Account");

            var complaints = _complaintService.GetAllComplaintByGymId(gymId);
            ViewData["CurrentGymId"] = gymId;

            var model = new ComplaintListViewModel
            {
                GymId = gymId,
                Complaints = complaints,
                OpenCount = complaints.Count(c => c.OwnerResponse == null),
                ResolvedCount = complaints.Count(c => c.OwnerResponse != null)
            };
            return View(model);
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
        public IActionResult MyComplaints()
        {

            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsMember() && !IsTrainer()) return RedirectToAction("Login", "Account");
            var userId = GetUserId() ?? 0;
            var complaint = _complaintService.GetAllComplaintByUserId(userId);

            ViewData["CurrentGymId"] = GetGymId() ?? 0;
            return View(complaint);
            
        }

        [HttpGet]
        public IActionResult Resolve(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsOwner() && !IsManager()) return RedirectToAction("Login", "Account");

            var complaint = _complaintService.GetcomplantById(id);
            if (complaint == null)
            {
                return null;
            }
            ResolveComplaintViewModel model = new ResolveComplaintViewModel
            {
                Id=complaint.Id,
                GymId=complaint.GymId,
                Subject=complaint.Subject,
                Message = complaint.Message,
                SubmittedByName = complaint.SubmittedByName,
                SubmittedByRole = complaint.SubmittedByRole,
                CreatedAt = complaint.CreatedAt
            };
            ViewData["CurrentGymId"] = GetGymId() ?? 0;
            return View(model);
        }
        [HttpPost]
        public IActionResult Resolve(ResolveComplaintViewModel model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsOwner() && !IsManager()) return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    foreach (var err in error.Value.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"FIELD: {error.Key} — ERROR: {err.ErrorMessage}");
                    }
                }
                var complaint = _complaintService.GetcomplantById(model.Id);
                if (complaint != null)
                {
                    model.Subject = complaint.Subject;
                    model.Message = complaint.Message;
                    model.SubmittedByName = complaint.SubmittedByName;
                    model.SubmittedByRole = complaint.SubmittedByRole;
                    model.CreatedAt = complaint.CreatedAt;
                }
                ViewData["CurrentGymId"] = model.GymId;

                return View(model);
            }
            _complaintService.Resolve(model.Id, model.OwnerResponse);
            return RedirectToAction("index", new { gymId = model.GymId });

        }
    }
}
