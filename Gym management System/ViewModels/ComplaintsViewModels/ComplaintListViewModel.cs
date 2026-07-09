using Gym_management_System.Models.Complaints;

namespace Gym_management_System.ViewModels.ComplaintsViewModels
{
    public class ComplaintListViewModel
    {
        public int GymId { get; set; }
        public IEnumerable<Complaint> Complaints { get; set; } = new List<Complaint>();
        public int OpenCount { get; set; }
        public int ResolvedCount { get; set; }
    }
}
