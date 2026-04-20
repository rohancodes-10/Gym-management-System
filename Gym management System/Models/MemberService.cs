namespace Gym_management_System.Models
{
    public class MemberService:IMemberService
    {
        private readonly ApplicationDbContext context;
        public MemberService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Members> GetAllMembers()
        {
            return context.Members;
        }
        public Members? GetMembers(int id)
        {
            return context.Members.Find(id);
        }
        public Members AddMember(Members members)
        {
            context.Members.Add(members);
            context.SaveChanges();
            return members;
        }
        public Members Update(Members changes)
        {
            var member = context.Members.Attach(changes);
            member.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changes;
        }
    }
}
