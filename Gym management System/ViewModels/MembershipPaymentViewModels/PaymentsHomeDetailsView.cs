using Gym_management_System.Models.Members;
using Gym_management_System.Models.MembershipPlans;

namespace Gym_management_System.ViewModels.MembershipPaymentViewModels
{
    public class PaymentsHomeDetailsView
    {
        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int MembershipPlanId { get; set; }
        public MembershipPlan MembershipPlan { get; set; }
        public Decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; } = "Active";
    }
}
