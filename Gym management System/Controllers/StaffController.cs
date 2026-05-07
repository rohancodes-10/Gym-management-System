using Gym_management_System.Models.Gyms;
using Gym_management_System.Models.Staffs;
using Gym_management_System.Models.Trainers;
using Gym_management_System.ViewModels.StaffViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;

namespace Gym_management_System.Controllers
{
    public class StaffController:BaseController
    {
        private readonly IStaffService staffService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public StaffController(IStaffService staffService, IWebHostEnvironment webHostEnvironment)
        {
            this.staffService = staffService;
            this.webHostEnvironment = webHostEnvironment;
        }
        private IActionResult? CheckAccess()
        {
            if (!IsLoggedIn()) return RedirectToAction("login", "Account");
            if (!IsOwner()) return RedirectToAction("login", "Account");
            return null; 
        }
        public IActionResult Index(int gymId)
        {
            var check = CheckAccess();
            if(check!=null) {return check;}
            var staff=staffService.GetAllStaffByGymId(gymId);
            var model = new StaffHomeViewModel
            {
                staffs = staff,
                gymId = gymId
            };
            return View(model);
        }
        public IActionResult Details(int id)
        {
            var check = CheckAccess();
            if (check != null) { return check; }
            var staff = staffService.GetStaff(id);
            var model = new StaffHomeViewModel
            {
                staff = staff
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Create(int gymid)
        {
            var check = CheckAccess();
            if (check != null) { return check; }
            var model = new CreateStaffViewModel
            {
                GymId=gymid
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateStaffViewModel model)
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
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }
            }
            Staff staff = new Staff
            {
                StaffAddress=model.StaffAddress,
                StaffName=model.StaffName,
                Age=model.Age,
                GymId=model.GymId,
                Gender=model.Gender,
                Phone=model.Phone,
                PhotoUrl=uniqueFileName,
                
            };
            staffService.Add(staff);
            return RedirectToAction("index", new { gymid = model.GymId });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var check = CheckAccess();
            if (check != null) { return check; }
            var staff = staffService.GetStaff(id);
            var model = new EditStaffViewModel
            {   id=staff.Id,
                StaffAddress = staff.StaffAddress,
                StaffName =staff.StaffName,
                Age = staff.Age,
                GymId = staff.GymId,
                Gender = staff.Gender,
                Phone = staff.Phone,
                ExistingPhotoUrl = staff.PhotoUrl,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditStaffViewModel model)
        {
            var check = CheckAccess();
            if (check != null) { return check; }
            if (ModelState.IsValid)
            {
                Staff? staff = staffService.GetStaff(model.id);
                staff.StaffAddress = model.StaffAddress;
                staff.StaffName = model.StaffName;
                staff.Age = model.Age;
                staff.GymId = model.GymId;
                staff.Gender = model.Gender;
                staff.Phone = model.Phone;

                string? uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
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
                    staff.PhotoUrl = uniqueFileName;
                }
                else
                {
                    staff.PhotoUrl = model.ExistingPhotoUrl;
                }
                staffService.Update(staff);
                return RedirectToAction("Details", new { id = model.id });
            }
            return View(model);   
        }
        public IActionResult Delete(int id)
        {
            var check = CheckAccess();
            if (check != null) { return check; }
            Staff staff=staffService.GetStaff(id);
            if (staff == null) 
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(staff.PhotoUrl))
            {
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", staff.PhotoUrl);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            staffService.Delete(id);
            return RedirectToAction("index", new { gymId = staff.GymId });
        }
    }
}
