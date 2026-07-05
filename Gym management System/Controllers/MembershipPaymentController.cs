using Gym_management_System.Models.Members;
using Gym_management_System.Models.MembershipPayments;
using Gym_management_System.Models.MembershipPlans;
using Gym_management_System.ViewModels.MembershipPaymentViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Gym_management_System.Controllers
{
    public class MembershipPaymentController:Controller
    {
        private readonly IMembershipPaymentService _membershipPaymentService;
        private readonly IMembershipPlansService _membershipPlansService;
        private readonly IMemberService _memberService;
        public MembershipPaymentController(IMemberService memberService,IMembershipPaymentService membershipPaymentService,IMembershipPlansService membershipPlansService)
        {
            _membershipPaymentService = membershipPaymentService;
            _membershipPlansService = membershipPlansService;
            _memberService= memberService;
        }
        public IActionResult Index(int gymid,string search,string? returnUrl)
        {
            _membershipPaymentService.UpdateExpired();
            var payments = _membershipPaymentService.GetAllPaymentsByGymId(gymid);
            if (!string.IsNullOrEmpty(search)) 
            {
                payments = payments.Where(p =>
                    p.Member.MemberName.Contains(search, StringComparison.OrdinalIgnoreCase)
                );
            }
            var model = new PaymentsHomeDetailsView
            {
                payments = payments,
                search = search,
                gymid = gymid,
                ReturnUrl = returnUrl
            };
            ViewData["CurrentGymId"] = gymid;
            return View(model);
        }
        [HttpGet]
        public IActionResult Create(int memberId)
        {

            var member=_memberService.GetMember(memberId);
            if (member == null)
            {
                return RedirectToAction("index");
            }
            var plans=_membershipPlansService.GetMembershipPlansByGymId(member.GymId);
            var model = new CreatePaymentViewModel
            {
                MemberId = member.Id,
                MemberName=member.MemberName,
                GymId=member.GymId,
                MembershipPlans = plans.Select(p => new SelectListItem
                {
                    Value = p.id.ToString(),
                    Text = $"{p.MembershipPlanName} - ${p.price} ({p.DurationInDays} days)"
                }).ToList()
            };
            ViewData["CurrentGymId"] = model.GymId;
            return View(model);
        }

        
        [HttpPost]
        public IActionResult Create(CreatePaymentViewModel model)
        {
            _membershipPaymentService.UpdateExpired();
            var ExistingActivePayment = _membershipPaymentService.GetActivePaymentByMemberId(model.MemberId);
            if (ExistingActivePayment != null)
            {
                TempData["Error"] = "The member already has active membership";
                return RedirectToAction("index", "member", new { gymid = model.GymId });
            }
            var plan = _membershipPlansService.GetMembershipPlanById(model.MembershipPlanId);
            var now=DateTime.UtcNow;
            var payment = new MembershipPayment
            {
                MemberId = model.MemberId,
                MembershipPlanId = model.MembershipPlanId,
                AmountPaid = plan.price,
                PaymentDate = now,
                StartDate = now,
                EndDate =now.AddDays(plan.DurationInDays),
                Status = "Active"
            };
            _membershipPaymentService.AddPayment(payment);
            ViewData["CurrentGymId"] = model.GymId;
            return RedirectToAction("index", new { gymid=model.GymId});
        }
        public IActionResult Activemembers(int gymId)
        {
            var members = _membershipPaymentService.GetActivemembersByGymId(gymId);
            ViewData["CurrentGymId"] = gymId;
            return View(members);
        }
        public IActionResult InActivemembers(int gymId)
        {
            var members = _membershipPaymentService.GetInActivemembersByGymId(gymId);
            ViewData["CurrentGymId"] = gymId;
            return View(members);
        }
        public IActionResult Delete(int id)
        {
            var payment = _membershipPaymentService.GetMembershipPaymentById(id);
            if (payment == null)
            {
                return NotFound();
            }
            int Gymid = payment.Member.GymId;
            _membershipPaymentService.Delete(id);
            ViewData["CurrentGymId"] = Gymid;
            return RedirectToAction("index", new { gymid =Gymid });
        }
    }

}
