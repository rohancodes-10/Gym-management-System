using Gym_management_System.Models.Trainers;
using Gym_management_System.Models.Members;
namespace Gym_management_System.ViewModels.TrainerViewModel
{
    public class TrainerHomeViewModels
    {
        public IEnumerable<Trainer> trainers { get; set; }
        public Trainer trainer { get; set; }
        public int gymid { get; set; }
        public IEnumerable<Member> AssignedMember { get; set; } =new List<Member>();
    }
}
