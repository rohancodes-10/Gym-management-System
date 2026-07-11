using Gym_management_System.Models.Members;
using Gym_management_System.ViewModels.AttendenceViewModels;

namespace Gym_management_System.Models.Attendences
{
    public class AttendenceService : IAttendence
    {
        private readonly ApplicationDbContext _context;

        public AttendenceService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<AttendenceRowViewModel> GetTodaysAttendenceStatus(int gymId) 
        {
            var today= DateOnly.FromDateTime(DateTime.Today);
            var member = _context.Members
                .Where(m => m.GymId == gymId)
                .ToList();
            var todaysAttendence = _context.Attendences
                .Where(a => a.Date == today && a.member.GymId == gymId)
                .ToList();
            return member.Select(m =>
            {
                var att = todaysAttendence.FirstOrDefault(a => a.MemberId == m.Id);
                return new AttendenceRowViewModel
                {
                    MemberId = m.Id,
                    MemberName = m.MemberName,
                    IsCheckedInToday = att != null,
                    AttendenceId = att?.Id,
                    CheckinTime = att?.CheckinTime,
                    CheckOutTime = att?.CheckOutTime
                };
            }).ToList();
        }
        public void MarkPresent(int memberId)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            bool alreadyMarked = _context.Attendences
                .Any(a => a.MemberId == memberId && a.Date == today);
            if (alreadyMarked) return;
            var attendance = new Attendence
            {
                MemberId = memberId,
                Date = today,
                CheckinTime = DateTime.Now
            };
            _context.Attendences.Add(attendance);
            _context.SaveChanges();
        }
        public void MarkCheckOut(int attendenceId)
        {
            var attendance = _context.Attendences.Find(attendenceId);
            if (attendance == null) return;

            attendance.CheckOutTime = DateTime.Now;
            _context.SaveChanges();
        }
    }
}