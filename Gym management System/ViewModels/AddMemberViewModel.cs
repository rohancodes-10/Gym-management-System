using Gym_management_System.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Gym_management_System.ViewModels
{
    public class AddMemberViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string MemberName { get; set; } = string.Empty;
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string city { get; set; }
        public int GymId { get; set; }
        public Gym Gym { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
