using Gym_management_System.Models;
using Gym_management_System.Models.Trainers;
using Gym_management_System.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Gym_management_System.Controllers
{
    public class TrainerController:Controller
    {
        public readonly ITrainerService trainerService;
        public readonly IWebHostEnvironment webHostEnvironment;
        public TrainerController(ITrainerService trainerService, IWebHostEnvironment webHostEnvironment)
        {
            this.trainerService = trainerService;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult index(int gymid)
        {
            var Trainer = trainerService.GetTrainersByGymId(gymid);
            var model = new TrainerHomeViewModels
            {
                trainers = Trainer,
                gymid = gymid
            };
            return View(model);
        }
    }
}
