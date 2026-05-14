using Microsoft.EntityFrameworkCore;

namespace Gym_management_System.Models.MembershipPayments
{
    public class MemberPaymentServicecs : IMembershipPaymentService
    {
        private readonly ApplicationDbContext _context;
        public MemberPaymentServicecs(ApplicationDbContext context)
        {
            _context = context;
        }
        public MembershipPayment GetMembershipPaymentById(int id)
        {
            return _context.MembershipPayments
                 .Include(p => p.Member)
                 .Include(p => p.MembershipPlan)
                 .FirstOrDefault(p => p.id == id);
        }
        public IEnumerable<MembershipPayment> GetMembershipPaymentByMemberId(int memberId)
        {
            return _context.MembershipPayments
                .Include(p => p.MembershipPlan)
                .Where(p => p.MemberId == memberId)
                .OrderByDescending(p => p.PaymentDate)
                .ToList();
        }
        public MembershipPayment AddPayment(int memberId,int planId)
        {
            var plan= _context.MembershipPlans.Find(planId);
            var Payment=new MembershipPayment
            {
                MemberId = memberId,
                MembershipPlanId = planId,
                AmountPaid = plan.price,
                PaymentDate = DateTime.Now,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(plan.DurationInDays),
                Status = "Active"
            };
            _context.MembershipPayments.Add(Payment);
            _context.SaveChanges();
            return Payment;
        }
        public void UpdateExpiredAsync()
        {
            var payments =  _context.MembershipPayments
                .Where(p => p.Status == "Active")
                .ToList();

            foreach (var payment in payments)
            {
                if (payment.EndDate < DateTime.Now)
                    payment.Status = "Expired";
            }

             _context.SaveChangesAsync();
        }
    }
}