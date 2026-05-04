namespace Gym_management_System.Models.Staffs
{
    public interface IStaffService
    {
         IEnumerable<Staff> GetAllStaffByGymId(int gymId);
         Staff GetStaff(int id);
        Staff Add (Staff staff);
         Staff Update(Staff changes);
        Staff Delete(int id);
    }
}
