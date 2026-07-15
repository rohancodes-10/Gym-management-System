namespace Gym_management_System.ViewModels.AttendenceViewModels
{
    public class CalenderDayViewModel
    {
        public DateOnly? Date { get; set; }
        public bool IsPresent { get; set; }
        public bool IsFuture { get; set; }
        public bool IsToday { get; set; }
     public TimeOnly? CheckIn { get; set; }
        public TimeOnly? CheckOut { get; set; }
    }
}
