using Gym_management_System.ViewModels.AttendenceViewModels;

namespace Gym_management_System.Models.Attendences
{
    public interface IAttendence
    {
        List<AttendenceRowViewModel> GetTodaysAttendenceStatus(int gymId);
        void MarkPresent(int memberId);
        void MarkCheckOut(int attendenceId);
        List<Attendence> GetMemberAttendenceForMonth(int memberId, int year, int month);
    }
}
