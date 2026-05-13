using Gym_management_System.Models.Gyms;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gym_management_System.ViewModels.MembershipPlanViewModels
{
    public class AddplanViewModel
    {

        [Required]
        public string MembershipPlanName { get; set; } = string.Empty;
        public int GymId { get; set; }
        public Gym Gym { get; set; }
        public int DurationInDays { get; set; }
        [Precision(10, 2)]
        public Decimal price { get; set; }
    }
}
