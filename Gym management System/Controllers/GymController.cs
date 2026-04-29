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
    }
}
