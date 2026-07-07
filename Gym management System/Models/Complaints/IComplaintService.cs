
namespace Gym_management_System.Models.Complaints
{
    public interface IComplaintService
    {
      public  List<Complaint> GetComplaintByGymId(int gymId);
      public  List<Complaint> GetComplaintByUserId(int userId);
       public Complaint? Getcomplaint(int id);
        public Complaint AddComplaint(Complaint complaint);
        Complaint? Resolve(int id, string ownerResponse);

    }
}
