using Gym_management_System.Models.Gyms;
namespace Gym_management_System.Models.Staffs
{
    public class Staff
    {
        public int Id { get; set; }
        public string StaffName { get; set; }
        public int Age { get; set; }
        public string StaffAddress { get; set; }
        public string Phone { get; set; }
        public int GymId { get; set; }
        public string Gender { get; set; }
        public Gym? gym { get; set; }
       
    }
}
