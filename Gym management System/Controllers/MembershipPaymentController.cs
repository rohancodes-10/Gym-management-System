using Gym_management_System.Models.MembershipPayments;
using Gym_management_System.Models.MembershipPlans;
using Gym_management_System.ViewModels.MembershipPaymentViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Gym_management_System.Controllers
{
    public class MembershipPaymentController:Controller
    {
        private readonly IMembershipPaymentService _membershipPaymentService;
        private readonly IMembershipPlansService _membershipPlansService;
        public MembershipPaymentController(IMembershipPaymentService membershipPaymentService,IMembershipPlansService membershipPlansService)
        {
            _membershipPaymentService = membershipPaymentService;
            _membershipPlansService = membershipPlansService;
        }
        public IActionResult Index(int gymid)
        {
            var payments = _membershipPaymentService.GetAllPaymentsByGymId(gymid);
            var model = new PaymentsHomeDetailsView
            {
                payments = payments
            };
            return View(model);
        }
        public IActionResult Create(int memberId,int planId)
        {
            var plan = _membershipPlansService.GetMembershipPlanById(planId);
            var payment = new MembershipPayment
            {
                MemberId = memberId,
                MembershipPlanId = planId,
                AmountPaid = plan.price,
                PaymentDate = DateTime.Now,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(plan.DurationInDays),
                Status = "Active"
            };
            _membershipPaymentService.AddPayment(payment);
            return RedirectToAction("index");
        }
    }
}
