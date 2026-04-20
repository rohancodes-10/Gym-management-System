using Gym_management_System.Models;

namespace Gym_management_System.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Member> members { get; set; }
        public Member? member { get; set; }
    }
}
