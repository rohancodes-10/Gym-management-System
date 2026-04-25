using Gym_management_System.Models;
using Gym_management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gym_management_System.Controllers
{
    public class MemberController: Controller
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberservice)
        {
            _memberService = memberservice;
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
    }
}
