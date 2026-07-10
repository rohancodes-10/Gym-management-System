using Gym_management_System.Models.Members;

namespace Gym_management_System.Models.Attendences
{
    public class Attendence
    {
        public int Id { get; set; }
        public int MemberId { get; set; }//foreign key 
        public Member member { get; set; }//navigating property
        public DateOnly Date { get; set; }
        public DateTime CheckinTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
    }
}
