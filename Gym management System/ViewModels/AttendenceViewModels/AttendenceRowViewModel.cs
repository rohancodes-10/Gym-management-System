namespace Gym_management_System.ViewModels.AttendenceViewModels
{
    public class AttendenceRowViewModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public bool IsCheckedInToday { get; set; }
        public int? AttendenceId { get; set; }
        public DateTime? CheckinTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
    }
}
