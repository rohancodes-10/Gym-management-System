namespace Gym_management_System.Models.Staffs
{
    public interface IStaffService
    {
        public IEnumerable<Staff> GetAllStaff();
        public Staff GetStaff(int id);
        public Staff Add (Staff staff);
        public Staff Update(Staff changes);
        public Staff Delete(int id);
    }
}
