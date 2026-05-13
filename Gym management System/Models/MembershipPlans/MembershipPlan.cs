using Gym_management_System.Models.Gyms;
using Microsoft.EntityFrameworkCore;
namespace Gym_management_System.Models.MembershipPlans
{
    public class MembershipPlan
    {
        public int id { get; set; }
        public string MembershipPlanName { get; set; }=string.Empty;
        public int GymId { get; set; }
        public Gym Gym { get; set; }
        public int DurationInDays { get; set; }
        [Precision(10,2)]
        public Decimal price { get; set; }
    }
}
