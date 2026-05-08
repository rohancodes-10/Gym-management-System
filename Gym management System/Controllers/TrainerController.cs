using Gym_management_System.Models;
using Gym_management_System.Models.Gyms;
using Gym_management_System.Models.Members;
using Gym_management_System.Models.Trainers;
using Gym_management_System.Models.Users;
using Gym_management_System.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace Gym_management_System.Controllers
{
    public class TrainerController:BaseController
    {
        private readonly ITrainerService trainerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAuthService _authService;
        public TrainerController(ITrainerService trainerService, IWebHostEnvironment webHostEnvironment,IAuthService authService)
        {
            this.trainerService = trainerService;
          _webHostEnvironment = webHostEnvironment;
            _authService = authService;
        }
        private IActionResult? CheckAccess()
        {
            if (!IsLoggedIn()) return RedirectToAction("login", "Account");
            if (!IsOwner()&&!IsManager()) return RedirectToAction("login", "Account");
            return null;
        }
        public IActionResult index(int gymid)
        {
            var check = CheckAccess();
            if (check != null) { return check; }
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

            if (!IsLoggedIn()) return RedirectToAction("login", "Account");
            if (!IsOwner() && !IsManager() && !IsTrainer()) return RedirectToAction("login","Account");
            if(!IsTrainer() && GetRoleId() !=id) return RedirectToAction("login", "Account");
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
            var check = CheckAccess();
            if (check != null) { return check; }
            var model = new AddTrainerViewModel
            {
                GymId =gymid
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddTrainerViewModel model)
        {
            var check = CheckAccess();
            if (check != null) { return check; }
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
            var user = new User
            {
                Name = model.TrainerName,
                Email = model.Email,
                RoleId=trainer.Id,
                Role="Trainer",
                GymId=trainer.Id,
                CreatedAt=DateTime.Now,
            };
            _authService.Register(user, model.Password);
            return RedirectToAction("index", new { gymid = model.GymId });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var check = CheckAccess();
            if (check != null) { return check; }
            Trainer? trainer = trainerService.GetTrainer(id);
            if (trainer == null)
            {
                return NotFound();
            }
            var model = new EditTrainerViewModel
            {
                TrainerName = trainer.TrainerName,
                TrainerAddress = trainer.TrainerAddress,
                Phone = trainer.Phone,
                Age = trainer.Age,
                GymId = trainer.GymId,
                ExistingPhotoUrl = trainer.PhotoUrl
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTrainerViewModel model)
        {
            var check = CheckAccess();
            if (check != null) { return check; }
            if (ModelState.IsValid)
            {
                Trainer? trainer = trainerService.GetTrainer(model.id);
                trainer.TrainerName = model.TrainerName;
                trainer.TrainerAddress = model.TrainerAddress;
                trainer.Phone = model.Phone;
                trainer.Age = model.Age;
                trainer.GymId = model.GymId;
                string? uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    if (!string.IsNullOrEmpty(model.ExistingPhotoUrl))
                    {
                        string oldFilePath = Path.Combine(uploadsFolder, model.ExistingPhotoUrl);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Photo.CopyToAsync(fileStream);
                    }
                    trainer.PhotoUrl = uniqueFileName;
                }
                else
                {
                    trainer.PhotoUrl = model.ExistingPhotoUrl;
                }

                trainerService.Update(trainer);

                return RedirectToAction("Details", new { Id = model.Id });
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var check = CheckAccess();
            if (check != null) { return check; }
            var trainer = trainerService.GetTrainer(id);
            if (trainer == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(trainer.PhotoUrl))
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", trainer.PhotoUrl);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            trainerService.Delete(id);
            return RedirectToAction("index", new { gymid = trainer.GymId });
        }
    }
}
