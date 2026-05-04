using Gym_management_System.Models.Staffs;
namespace Gym_management_System.ViewModels.StaffViewModels
{
    public class StaffHomeViewModel
    {
        public IEnumerable<Staff> staffs { get; set; }
        public Staff staff {  get; set; }
        public int gymId { get; set; }
    }
}
