using Gym_management_System.Models.Gyms;
namespace Gym_management_System.Models.MembershipPlan
{
    public class MembershipPlan
    {
        public int id { get; set; }
        public string MembershipPlanName { get; set; }=string.Empty;
        public int GymId { get; set; }
        public Gym Gym { get; set; }
        public int DurationInDays { get; set; }
        public Decimal price { get; set; }
    }
}
