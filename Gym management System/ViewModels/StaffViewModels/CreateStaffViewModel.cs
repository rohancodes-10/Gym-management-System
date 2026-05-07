using Gym_management_System.Models.Gyms;

namespace Gym_management_System.ViewModels.StaffViewModels
{
    public class CreateStaffViewModel
    {
        public string StaffName { get; set; }
        public int Age { get; set; }
        public string StaffAddress { get; set; }
        public string Phone { get; set; }
        public int GymId { get; set; }
        public string Gender { get; set; }
        public Gym? gym { get; set; }
        public IFormFile? Photo { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }
    }
}
