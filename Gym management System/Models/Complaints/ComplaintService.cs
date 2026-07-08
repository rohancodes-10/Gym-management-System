using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Gym_management_System.Models.Complaints
{
    public class ComplaintService : IComplaintService
    {
        public readonly ApplicationDbContext _context;
        public ComplaintService(ApplicationDbContext context) {
            _context = context;
        }
        public List<Complaint> GetAllComplaintByGymId(int gymId)
        {
            return _context.Complaints
                .Where(p=>p.GymId == gymId)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }
        public List<Complaint> GetAllComplaintByUserId(int userId)
        {
            return _context.Complaints
                .Where(c => c.SubmittedById == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();
        }
        public Complaint GetcomplantById(int id)
        {
            return _context.Complaints.Find(id);
        }
        public Complaint AddComplaint(Complaint complaint)
        {
            _context.Complaints.Add(complaint);
            _context.SaveChanges();
            return complaint;
        }
        public Complaint Resolve(int id,string ownerResponse)
        {
            var complaint = _context.Complaints.Find(id);
            if(complaint == null) {
                return null;
            }
            complaint.OwnerResponse= ownerResponse;
            complaint.ResolvedAt= DateTime.Now;
            _context.SaveChanges();
            return complaint;
        }
    }
}
