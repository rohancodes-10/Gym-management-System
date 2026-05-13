using Gym_management_System.Models.MembershipPlans;

namespace Gym_management_System.ViewModels.MembershipPlanViewModels
{
    public class PlansHomeViewModels
    {
        public IEnumerable<MembershipPlan> Plans { get; set; }
        public MembershipPlan plan { get; set; }
        public int gymid { get; set; }
    }
}
