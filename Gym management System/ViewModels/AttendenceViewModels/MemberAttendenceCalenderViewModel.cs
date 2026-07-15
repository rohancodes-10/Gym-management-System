namespace Gym_management_System.ViewModels.AttendenceViewModels
{
    public class MemberAttendenceCalenderViewModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM yyyy");
        public List<CalenderDayViewModel> Days { get; set; } = new();
        public int TotalPresentDays { get; set; }
        public int TotalDaysElapsed { get; set; }
    }
}
