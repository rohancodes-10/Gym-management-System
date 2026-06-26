using Gym_management_System.Models.Gyms;
using System.ComponentModel.DataAnnotations;

namespace Gym_management_System.ViewModels.TrainerViewModel
{
    public class AddTrainerViewModel
    {
        [Display(Name ="Name")]
        public string TrainerName { get; set; }
        public string Phone { get; set; }
        public int? Id { get; set; }
        public string TrainerAddress { get; set; }
        public int Age { get; set; }
        public int GymId { get; set; }
       
        public string Email { get; set; }=string.Empty;
        public string Password { get; set; } =string.Empty;
    }
}
