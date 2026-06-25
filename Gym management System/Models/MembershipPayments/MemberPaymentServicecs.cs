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
        public IEnumerable<MembershipPayment> GetAllPayments()
        {
            return _context.MembershipPayments
                .Include(p => p.Member)
                .Include(p => p.MembershipPlan)
                .OrderByDescending(p => p.PaymentDate)
                .ToList();
        }
        public IEnumerable<MembershipPayment> GetAllPaymentsByGymId(int gymid)
        {
            return _context.MembershipPayments
                .Include(p => p.Member)
                .Include(p => p.MembershipPlan)
                .Where(p=>p.Member.GymId==gymid)
                .OrderByDescending(p => p.PaymentDate)
                .ToList();
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
        public MembershipPayment GetActivePaymentByMemberId(int memberid)
        {
            return _context.MembershipPayments
                .Where(p => p.MemberId == memberid && p.Status == "Active")
                .FirstOrDefault();
        }
        public MembershipPayment AddPayment(MembershipPayment payment)
        {
       
            _context.MembershipPayments.Add(payment);
            _context.SaveChanges();
            return payment;
        }
        public IEnumerable<MembershipPayment> GetActivemembersByGymId(int gymId)
        {
            _context.MembershipPayments
                .Include(p => p.Member)
                .Include(p => p.MembershipPlan)
                .Where(p => p.Member.GymId == gymId && p.EndDate >= DateTime.Now)
                .OrderByDescending(p => p.PaymentDate)
                .ToList();
        }
        public void UpdateExpired()
        {
            var payments =  _context.MembershipPayments
                .Where(p => p.Status == "Active")
                .ToList();

            foreach (var payment in payments)
            {
                if (payment.EndDate < DateTime.Now)
                    payment.Status = "Expired";
            }

             _context.SaveChanges();
        }
    }
}