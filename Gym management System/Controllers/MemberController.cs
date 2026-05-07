using Gym_management_System.Models.Members;
using Gym_management_System.Models.Users;
using Gym_management_System.ViewModels.Members;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;
using System.Reflection;

namespace Gym_management_System.Controllers
{
    public class MemberController : BaseController
    {
        private readonly IMemberService _memberService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAuthService _authService;
        public MemberController(IMemberService memberservice, IWebHostEnvironment webHostEnvironment,IAuthService authService)
        {
            _memberService = memberservice;
            _webHostEnvironment = webHostEnvironment;
            _authService = authService;
        }
        private IActionResult? CheckAccess()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsOwner() && !IsManager()) return RedirectToAction("Login", "Account");
            return null;
        }
        public IActionResult Index(int gymid)
        {
            var check = CheckAccess();
            if (check != null) return check;
            var member = _memberService.GetAllMembersByGymId(gymid);
            HomeViewModel homeViewModel = new HomeViewModel
            {
                members = member,
                gymid = gymid
            };
            return View(homeViewModel);
        }
        public IActionResult Details(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            // Member can only see their own profile
            if (IsMember() && GetRoleId() != id)
                return RedirectToAction("Login", "Account");

            // Owner and Manager can see anyone
            if (!IsOwner() && !IsManager() && !IsMember())
                return RedirectToAction("Login", "Account");

            var members = _memberService.GetMember(id);
            HomeViewModel homeViewModel = new HomeViewModel
            {
                member = members
            };
            return View(homeViewModel);
        }
        [HttpGet]
        public IActionResult Create(int gymid)
        {
            var check = CheckAccess();
            if (check != null) return check;
            AddMemberViewModel model = new AddMemberViewModel
            {
                GymId = gymid
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddMemberViewModel model)
        {
            var check = CheckAccess();
            if (check != null) return check;
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
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
            var member = new Member
            {
                MemberName = model.MemberName,
                Address = model.Address,
                Phone = model.Phone,
                Gender = model.Gender,
                Age = model.Age,
                city = model.city,
                GymId = model.GymId,
                PhotoUrl = uniqueFileName
            };
            _memberService.AddMember(member);
            var user = new User
            {
                Name=model.MemberName,
                Email = model.Email,
                Role="Member",
                RoleId=member.Id,
                GymId=model.GymId,
                CreatedAt = DateTime.Now,
            };
            _authService.Register(user, model.Password);
            return RedirectToAction("index" ,new {gymid=model.GymId });
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var check = CheckAccess();
            if (check != null) return check;
            Member? member = _memberService.GetMember(Id);
            if (member == null)
                return NotFound();
            MemberEditViewModel memberEditViewModel = new MemberEditViewModel
            {
                Id = member.Id,
                MemberName = member.MemberName,
                Address = member.Address,
                Phone = member.Phone,
                Gender = member.Gender,
                Age = member.Age,
                city = member.city,
                GymId = member.GymId,
                ExistingPhotoUrl = member.PhotoUrl
            };
            return View(memberEditViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MemberEditViewModel model)
        {
            var check = CheckAccess();
            if (check != null) return check;
            if (ModelState.IsValid)
            {
                Member member = _memberService.GetMember(model.Id);
                member.MemberName = model.MemberName;
                member.Address = model.Address;
                member.Phone = model.Phone;
                member.Gender = model.Gender;
                member.Age = model.Age;
                member.city = model.city;
                member.GymId = model.GymId;
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
                    member.PhotoUrl = uniqueFileName;
                }
                else
                {
                    member.PhotoUrl = model.ExistingPhotoUrl;
                }
                _memberService.Update(member);
                return RedirectToAction("Details", new { Id = model.Id });
            }
            return View(model);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var check = CheckAccess();
            if (check != null) return check;
            Member? member = _memberService.GetMember(id);
            if (member == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(member.PhotoUrl))
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", member.PhotoUrl);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            _memberService.Delete(id);
            return RedirectToAction("index", new {gymid=member.GymId});
        }

            
        }

    }

