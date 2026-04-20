namespace Gym_management_System.Models
{
    public interface IMemberService
    {
        IEnumerable<Members> GetAllMembers();
        Members? GetMembers(int id);
        Members AddMember(Members members);
        Members Update(Members changes);
    }
}
