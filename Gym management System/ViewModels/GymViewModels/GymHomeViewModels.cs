using Gym_management_System.Models.Gyms;

namespace Gym_management_System.ViewModels.GymViewModels
{
    public class GymHomeViewModels
    {
        public IEnumerable<Gym> Gyms {  get; set; }
        public Gym gym { get; set; }
        public int TotalActiveMembers { get; set; }
        public int TotalInactiveMembers { get; set; }
    }
}
