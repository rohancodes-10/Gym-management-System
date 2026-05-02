using Gym_management_System.Models;
using Gym_management_System.Models.Trainers;
using Gym_management_System.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Gym_management_System.Controllers
{
    public class TrainerController:Controller
    {
        public readonly ITrainerService trainerService;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public TrainerController(ITrainerService trainerService, IWebHostEnvironment webHostEnvironment)
        {
            this.trainerService = trainerService;
          _webHostEnvironment = webHostEnvironment;
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
        public IActionResult Details(int id)
        {
            var trainer = trainerService.GetTrainer(id);
            var model = new TrainerHomeViewModels
            {
                trainer = trainer
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create(int gymid) 
        {
            var model = new AddTrainerViewModel
            {
                GymId =gymid
            };
            return View(model);
        }
        public async Task<IActionResult> Create(AddTrainerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string? uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }
            }
            Trainer trainer = new Trainer
            {
                TrainerName=model.TrainerName,
                TrainerAddress=model.TrainerAddress,
                Phone=model.Phone,
                Age=model.Age,
                GymId = model.GymId,
                PhotoUrl=uniqueFileName
            };
            trainerService.AddTrainer(trainer);
            return RedirectToAction("index", new { gymid = model.GymId });
        }
    }
}
