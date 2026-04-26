using Gym_management_System.Models;
using Gym_management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gym_management_System.Controllers
{
    public class MemberController: Controller
    {
        private readonly IMemberService _memberService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MemberController(IMemberService memberservice, IWebHostEnvironment webHostEnvironment )
        {
            _memberService = memberservice;
           _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var member = _memberService.GetAllMembers();
            HomeViewModel homeViewModel = new HomeViewModel
            {
                members = member
            };
            return View(homeViewModel);
        }
        public IActionResult Details(int id)
        {
            var members = _memberService.GetMember(id);
            HomeViewModel homeViewModel = new HomeViewModel
            {
                member = members
            };
            return View(homeViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddMemberViewModel model)
        {
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
                return RedirectToAction("index");
            }


    }
}
