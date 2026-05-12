using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<Member> GetAllMembersByTrainerId(int trainerId)
        {
            return context.Members
                .Where(m => m.TrainerId == trainerId)
                .ToList();
        }
        public Member? GetMember(int id)
        {
            return context.Members.Find(id);
                
        }
        public Member? GetMemberWithTrainer(int id)
        {
            return context.Members
                .Include(m => m.Trainer)
                .FirstOrDefault(m => m.Id == id);
        }
        public Member AddMember(Member members)
        {
            context.Members.Add(members);
            context.SaveChanges();
            return members;
        }
        public Member Update(Member changes)
        {
            context.Entry(changes).State = EntityState.Modified;
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
