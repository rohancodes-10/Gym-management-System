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
        private readonly IMemberService _memberService;
        public TrainerController(ITrainerService trainerService,IMemberService memberService, IWebHostEnvironment webHostEnvironment,IAuthService authService)
        {
            this.trainerService = trainerService;
          _webHostEnvironment = webHostEnvironment;
            _authService = authService;
            _memberService = memberService;
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
            if (IsOwner() || IsManager())
            {
                var trainers = trainerService.GetTrainer(id);
                if (trainers == null)
                {
                    return NotFound();
                }
                var members = _memberService.GetAllMembersByTrainerId(id);
                return View(new TrainerHomeViewModels { trainer = trainers,AssignedMember=members });
            }
            if(IsTrainer())
            {
                if (GetRoleId() != id)
                {
                    return RedirectToAction("login", "Account");
                }
                var trainers = trainerService.GetTrainer(id);
                if (trainers == null)
                {
                    return NotFound();
                }
                var members = _memberService.GetAllMembersByTrainerId(id);
                if (members == null)
                {
                    return NotFound();
                }
                return View(new TrainerHomeViewModels { trainer = trainers, AssignedMember = members });
            }
            if(IsMember())
            {
                var member = _memberService.GetMember(GetRoleId().Value);
                if (member == null || member.TrainerId != id)
                {
                    return NotFound();
                }
                var trainers = trainerService.GetTrainer(id);
                if (trainers == null)
                {
                    return NotFound();
                }
                var members = _memberService.GetAllMembersByTrainerId(id);
               
                return View(new TrainerHomeViewModels { trainer = trainers, AssignedMember = members });
            }
            
            return RedirectToAction("Login", "Account");
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
          
            Trainer trainer = new Trainer
            {
                TrainerName=model.TrainerName,
                TrainerAddress=model.TrainerAddress,
                Phone=model.Phone,
                Age=model.Age,
                GymId = model.GymId,
               
            };
            trainerService.AddTrainer(trainer);
            var user = new User
            {
                Name = model.TrainerName,
                Email = model.Email,
                RoleId=trainer.Id,
                Role="Trainer",
                GymId=model.GymId,
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
           
            trainerService.Delete(id);
            return RedirectToAction("index", new { gymid = trainer.GymId });
        }
    }
}
