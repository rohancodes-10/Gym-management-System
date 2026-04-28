using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Gym_management_System.Models.Gyms;
namespace Gym_management_System.Models.Members
{
    public class Member
    {
        public int Id { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string city { get; set; }
        public int GymId { get; set; }
        public Gym Gym { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
