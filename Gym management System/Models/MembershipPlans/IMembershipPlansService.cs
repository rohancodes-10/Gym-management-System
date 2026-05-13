namespace Gym_management_System.Models.MembershipPlans
{
    public interface IMembershipPlansService
    {
        IEnumerable<MembershipPlan> GetMembershipPlansByGymId(int gymid);
        MembershipPlan GetMembershipPlanById(int id);
        MembershipPlan AddPlan(MembershipPlan plan);
        MembershipPlan UpdatePlan(MembershipPlan changes);
        MembershipPlan DeletePlan(MembershipPlan plan);
    }
}
