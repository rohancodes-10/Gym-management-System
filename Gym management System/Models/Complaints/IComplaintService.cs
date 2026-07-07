
namespace Gym_management_System.Models.Complaints
{
    public interface IComplaintService
    {
      public  List<Complaint> GetAllComplaintByGymId(int gymId);
      public  List<Complaint> GetAllComplaintByUserId(int userId);
        public Complaint GetcomplantById(int id);
        public Complaint AddComplaint(Complaint complaint);
        Complaint? Resolve(int id, string ownerResponse);

    }
}
