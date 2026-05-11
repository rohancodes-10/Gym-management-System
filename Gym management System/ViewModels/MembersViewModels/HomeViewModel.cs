using Gym_management_System.Models.Members;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym_management_System.ViewModels.Members
{
    public class HomeViewModel
    {
        public IEnumerable<Member> members { get; set; }
        public Member? member { get; set; }
        public int gymid { get; set; }
        public int TrainerId { get; set; }
        public List<SelectListItem> Trainers { get; set; }
    }
}
