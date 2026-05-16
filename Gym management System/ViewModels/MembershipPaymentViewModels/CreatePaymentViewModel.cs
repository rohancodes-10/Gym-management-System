using Gym_management_System.Models.Members;
using Gym_management_System.Models.MembershipPlans;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym_management_System.ViewModels.MembershipPaymentViewModels
{
    public class CreatePaymentViewModel
    {
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public string MemberName { get; set; }
        public int MembershipPlanId { get; set; }
      public List<SelectListItem> MembershipPlans { get; set; }
      
        public int GymId { get; set; }
    }
}
