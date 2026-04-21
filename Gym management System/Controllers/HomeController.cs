using Gym_management_System.Models;
using Gym_management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gym_management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemberService _memberservice;

        public HomeController(IMemberService memberService)
        {
            _memberservice = memberService;
        }

        public IActionResult Index()
        {
            var member = _memberservice.GetAllMembers();
            HomeViewModel homeViewModel = new HomeViewModel
            {
                members = member
            };
            return View(homeViewModel);
        }
        public IActionResult Details(int id)
        {
            var members = _memberservice.GetMember(id);
            HomeViewModel homeViewModel = new HomeViewModel
            {
                member =members
            };
            return View(homeViewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
