using System.Threading.Channels;

namespace Gym_management_System.Models.MembershipPlans
{
    public class MembershipPlansService : IMembershipPlansService
    {
        private readonly ApplicationDbContext _context;
        public MembershipPlansService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<MembershipPlan> GetMembershipPlansByGymId(int gymid)
        {
            return _context.MembershipPlans
                .Where(a => a.GymId == gymid)
                .ToList();
        }
         public MembershipPlan GetMembershipPlanById(int id)
        {
          return _context.MembershipPlans.Find(id);
        }
        public MembershipPlan AddPlan(MembershipPlan plans)
        {
            _context.MembershipPlans.Add(plans);
            _context.SaveChanges();
            return plans;
        }
        public MembershipPlan UpdatePlan(MembershipPlan changes) 
        {
            _context.MembershipPlans.Update(changes);
            _context.SaveChanges();
            return changes;
        }
        public MembershipPlan DeletePlan(int id)
        {
            var plan=_context.MembershipPlans.Find(id);
            if (plan != null)
            {
                _context.MembershipPlans.Remove(plan);
                _context.SaveChanges();
                return plan;
            }
            return null;
        }
    }
}
