using Gym_management_System.Models.Members;
using Gym_management_System.Models.MembershipPayments;
using Gym_management_System.Models.MembershipPlans;

namespace Gym_management_System.ViewModels.MembershipPaymentViewModels
{
    public class PaymentsHomeDetailsView
    {
        public IEnumerable<MembershipPayment> payments { get; set; }
        public MembershipPlan membershipPlan { get; set; }
        public int gymid { get; set; }
    }
}
