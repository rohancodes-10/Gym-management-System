namespace Gym_management_System.Models.Members
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
