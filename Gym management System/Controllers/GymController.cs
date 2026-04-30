using Gym_management_System.Models.Gyms;
using Gym_management_System.ViewModels.GymViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gym_management_System.Controllers
{
    public class GymController:Controller
    {
        private readonly IGymService gymService;
        public GymController(IGymService gymService)
        {
           this.gymService = gymService;
        }
        public IActionResult Index()
        {
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
    }
}
