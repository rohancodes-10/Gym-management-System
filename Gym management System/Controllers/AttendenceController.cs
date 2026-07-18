using Gym_management_System.Models.Attendences;
using Gym_management_System.Models.Members;
using Gym_management_System.ViewModels.AttendenceViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gym_management_System.Controllers
{
    public class AttendenceController:BaseController
    {
        private readonly IAttendence attendenceService;
        public AttendenceController(IAttendence _attendenceService) 
        {
            attendenceService= _attendenceService;
        }
        public IActionResult index(int gymId)
        {
            ViewData["CurrentGymId"] = gymId;
            var model=attendenceService.GetTodaysAttendenceStatus(gymId);
            return View(model);
        }
        [HttpPost]
        public IActionResult MarkPresent(int memberId,int gymId)
        {
            attendenceService.MarkPresent(memberId);
            return RedirectToAction("index", new { gymId });

        }
        [HttpPost]
        public IActionResult MarkCheckOut(int attendenceId,int gymId)
        {
            attendenceService.MarkCheckOut(attendenceId);
            return RedirectToAction("index", new { gymId });

        }
        [HttpGet]
        public IActionResult MyAttendance(int? month, int? year)
        {
            if (GetRole() != "Member") return Forbid();

            var memberId = HttpContext.Session.GetInt32("RoleId");
            if (memberId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var today = DateOnly.FromDateTime(DateTime.Today);
            int y = year ?? today.Year;
            int m = month ?? today.Month;

            var records = attendenceService.GetMemberAttendenceForMonth(memberId.Value, y, m);
            var recordsByDate = records.ToDictionary(a => a.Date, a => a);

            var firstOfMonth = new DateOnly(y, m, 1);
            int daysInMonth = DateTime.DaysInMonth(y, m);
            int leadingBlanks = (int)firstOfMonth.DayOfWeek; // Sunday = 0

            var vm = new MemberAttendenceCalenderViewModel { Month = m, Year = y };

            for (int i = 0; i < leadingBlanks; i++)
                vm.Days.Add(new CalenderDayViewModel()); // padding

            for (int d = 1; d <= daysInMonth; d++)
            {
                var date = new DateOnly(y, m, d);
                bool isFuture = date > today;
                recordsByDate.TryGetValue(date, out var record);

                vm.Days.Add(new CalenderDayViewModel
                {
                    Date = date,
                    IsFuture = isFuture,
                    IsToday = date == today,
                    IsPresent = record != null,
                    CheckIn = record != null
    ? TimeOnly.FromDateTime(record.CheckinTime)
    : null,
                    CheckOut = record?.CheckOutTime.HasValue == true
    ? TimeOnly.FromDateTime(record.CheckOutTime.Value)
    : null
                });

                if (!isFuture)
                {
                    vm.TotalDaysElapsed++;
                    if (record != null) vm.TotalPresentDays++;
                }
            }

            return View(vm);
        }
    }
}
