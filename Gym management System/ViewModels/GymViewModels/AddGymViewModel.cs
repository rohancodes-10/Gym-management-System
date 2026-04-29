using System.ComponentModel.DataAnnotations;

namespace Gym_management_System.ViewModels.GymViewModels
{
    public class AddGymViewModel
    {
        public int Id { get; set; }

        [Required]
        public string GymName { get; set; } = string.Empty;
        public string GymAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
