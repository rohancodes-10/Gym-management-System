namespace Gym_management_System.Models
{
    public class MemberService:IMemberService
    {
        private readonly ApplicationDbContext context;
        public MemberService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Member> GetAllMembers()
        {
            return context.Members.ToList();
        }
        public Member? GetMember(int id)
        {
            return context.Members.Find(id);
        }
        public Member AddMember(Member members)
        {
            context.Members.Add(members);
            context.SaveChanges();
            return members;
        }
        public Member Update(Member changes)
        {
            var member = context.Members.Attach(changes);
            member.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changes;
        }
    }
}
