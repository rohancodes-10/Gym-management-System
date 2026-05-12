namespace Gym_management_System.Models.Members
{
    public interface IMemberService
    {
        IEnumerable<Member> GetAllMembersByGymId(int gymid);
        IEnumerable<Member> GetAllMembersByTrainerId(int trainerId);
        Member? GetMember(int id);
        Member AddMember(Member member);
        Member Update(Member changes);
        Member Delete(int id);
        Member? GetMemberWithTrainer(int id);
    }
}
