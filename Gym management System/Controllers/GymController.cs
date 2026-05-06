using Gym_management_System.Models.Gyms;
using Gym_management_System.ViewModels.GymViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Gym_management_System.Controllers
{
    public class GymController:BaseController
    {
        private readonly IGymService gymService;
        public GymController(IGymService gymService)
        {
           this.gymService = gymService;
        }
        public IActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsOwner()) return RedirectToAction("Login", "Account");
            var gym = gymService.GetAllGyms();
            GymHomeViewModels gymHomeViewModels = new GymHomeViewModels
            {
                Gyms = gym
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
            var gym = gymService.GetGym(id);
            GymHomeViewModels gymHomeViewModels = new GymHomeViewModels
            {
                gym = gym
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
