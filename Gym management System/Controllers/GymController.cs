using Gym_management_System.Models.Gyms;
using Gym_management_System.Models.MembershipPayments;
using Gym_management_System.ViewModels.GymViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Gym_management_System.Controllers
{
    public class GymController:BaseController
    {
        private readonly IGymService gymService;
        private readonly IMembershipPaymentService _membershipPaymentService;
        //private IActionResult? CheckAccess()
        //{
        //    if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        //    if (!IsOwner() && !IsManager()) return RedirectToAction("Login", "Account");
        //    return null;
        //}
        public GymController(IGymService gymService,IMembershipPaymentService membershipPaymentService)
        {
           this.gymService = gymService;
            _membershipPaymentService = membershipPaymentService;
        }
        public IActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsOwner()) return RedirectToAction("Login", "Account");
            var gyms = gymService.GetAllGymswithDetails();
            GymHomeViewModels gymHomeViewModels = new GymHomeViewModels
            {
                Gyms = gyms,
                TotalGyms=gyms.Count(),
                TotalMembers = gyms.Sum(g => g.members?.Count ?? 0),
                TotalTrainers=gyms.Sum(g=>g.trainers?.Count?? 0)
            };
            return View(gymHomeViewModels);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddGymViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var gym = new Gym
            {
                GymName=model.GymName,
                GymAddress=model.GymAddress,
                Phone=model.Phone,
                City=model.City
            };
            gymService.AddGym(gym);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsOwner() && !IsManager()) return RedirectToAction("Login", "Account");
            _membershipPaymentService.UpdateExpired();
            var payments = _membershipPaymentService.GetAllPaymentsByGymId(id);
            var latestPerMember = payments
            .GroupBy(p => p.MemberId)
             .Select(g => g.OrderByDescending(p => p.EndDate).First());

            var gym = gymService.GetGym(id);
            GymHomeViewModels gymHomeViewModels = new GymHomeViewModels
            {
                gym = gym,
                TotalActiveMembers = latestPerMember.Count(p => p.Status == "Active"),
                TotalInactiveMembers=latestPerMember.Count(p=>p.Status=="Inactive")
            };
            
            return View(gymHomeViewModels);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var gym = gymService.GetGym(id);
            if (gym == null)
            {
                return NotFound();
            }
            EditGymViewModel editGymViewModel = new EditGymViewModel
            {
                Id = gym.Id,
                GymName = gym.GymName,
                GymAddress = gym.GymAddress,
                City = gym.City,
                Phone = gym.Phone
            };
            return View(editGymViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditGymViewModel model)
        {
            if (ModelState.IsValid)
            {
                Gym gym = gymService.GetGym(model.id);
                gym.GymName = model.GymName;
                gym.GymAddress = model.GymAddress;
                gym.City = model.City;
                gym.Phone = model.Phone;
                gymService.Update(gym);
                return RedirectToAction("Details", new { id = model.id });
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Gym? gym = gymService.GetGym(id);
            if (gym == null)
            {
                return NotFound();
            }
            gymService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
