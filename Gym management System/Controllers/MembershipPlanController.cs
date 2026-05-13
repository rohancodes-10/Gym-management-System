using Gym_management_System.Models.MembershipPlans;
using Gym_management_System.ViewModels.MembershipPlanViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Gym_management_System.Controllers
{
    public class MembershipPlanController:Controller
    {
        private readonly IMembershipPlansService _membershipPlansService;
        public MembershipPlanController(IMembershipPlansService membershipPlansService)
        {
            _membershipPlansService = membershipPlansService;
        }
        public ActionResult Index(int gymid) 
        {
            var plans = _membershipPlansService.GetMembershipPlansByGymId(gymid);
            var model = new PlansHomeViewModels
            {
                Plans = plans,
                gymid=gymid
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Create(int gymid)
        {
            var model = new AddplanViewModel
            {
                GymId = gymid
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(AddplanViewModel model)
        {
            if (model == null)
            {
                return null;
            }
            var MembershipPlan = new MembershipPlan
            {
                MembershipPlanName = model.MembershipPlanName,
                GymId=model.GymId,
                price=model.price,
                DurationInDays=model.DurationInDays
            };
            _membershipPlansService.AddPlan(MembershipPlan);
            return RedirectToAction("index", new { gymid = model.GymId });
        }
        [HttpGet]
        public IActionResult Edit (int id)
        {
            var plans = _membershipPlansService.GetMembershipPlanById(id);
            var model = new EditPlanViewModel
            {
                Id =id,
                GymId=plans.GymId,
                MembershipPlanName=plans.MembershipPlanName,
                price = plans.price,
                DurationInDays = plans.DurationInDays
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditPlanViewModel model)
        {
            var plan= _membershipPlansService.GetMembershipPlanById(model.Id);
            if (plan == null)
            {
                return NotFound();
            }
            plan.MembershipPlanName = model.MembershipPlanName;
            plan.price = model.price;
            plan.DurationInDays = model.DurationInDays;
            _membershipPlansService.UpdatePlan(plan);
            return RedirectToAction("index", new { gymid = model.GymId });
        }
        public IActionResult Delete(int id)
        {
            var plan = _membershipPlansService.GetMembershipPlanById(id);
            if (plan == null)
            {
                return NotFound();
            }
            _membershipPlansService.DeletePlan(id);
            return RedirectToAction("index", new { gymid = plan.GymId });
        }
    }
}
