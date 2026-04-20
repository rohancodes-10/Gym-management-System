using Gym_management_System.Models;

namespace Gym_management_System.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Members> members { get; set; }
        public Members? member { get; set; }
    }
}
