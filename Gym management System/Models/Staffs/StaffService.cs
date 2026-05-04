using Microsoft.AspNetCore.Http.HttpResults;

namespace Gym_management_System.Models.Staffs
{
    public class StaffService:IStaffService
    {
        private readonly ApplicationDbContext _context;
       
        public StaffService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Staff> GetAllStaffByGymId(int gymId) 
        {
           return _context.Staffs
                .Where(g=>g.GymId == gymId)
                .ToList();
        }
        public Staff GetStaff(int id)
        {
            return _context.Staffs.Find(id);
        }
        public Staff Add(Staff staff) 
        {
            _context.Staffs.Add(staff);
            _context.SaveChanges();
            return (staff);  
        }
        public Staff Update(Staff changes) 
        {
            var staff=_context.Staffs.Attach(changes);
            staff.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return (changes);
        }
        public Staff Delete(int id) 
        {
            var staff = _context.Staffs.Find(id);
            if (staff == null)
            {
                return null;
            }
                _context.Staffs.Remove(staff);
                _context.SaveChanges();
                return staff;
          
        }
    }
}
