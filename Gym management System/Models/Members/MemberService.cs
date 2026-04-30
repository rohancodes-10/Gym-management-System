using Microsoft.AspNetCore.Http.HttpResults;

namespace Gym_management_System.Models.Members
{
    public class MemberService:IMemberService
    {
        private readonly ApplicationDbContext context;
        public MemberService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Member> GetAllMembersByGymId(int gymId)
        {
            return context.Members
                .Where(m=>m.GymId==gymId)
                .ToList();
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
        public Member Delete(int id)
        {
            var member = context.Members.Find(id);
            if (member == null)
            {
                return null;
            }
            context.Members.Remove(member);
            context.SaveChanges();
            return member;
        }
    }
}
