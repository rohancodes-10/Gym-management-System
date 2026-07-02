using System.ComponentModel.DataAnnotations;

namespace Gym_management_System.ViewModels.Members
{
    public class MemberEditViewModel
    {
        public int Id { get; set; }

        public string MemberName { get; set; } = string.Empty;
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string city { get; set; }
        public int GymId { get; set; }



    }
}
