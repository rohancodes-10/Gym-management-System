namespace Gym_management_System.Models
{
    public interface IMemberService
    {
        IEnumerable<Member> GetAllMembers();
        Member? GetMember(int id);
        Member AddMember(Member member);
        Member Update(Member changes);
        Member Delete(int id);
    }
}
