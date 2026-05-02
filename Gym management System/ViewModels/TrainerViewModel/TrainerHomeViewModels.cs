using Gym_management_System.Models.Trainers;

namespace Gym_management_System.ViewModels.TrainerViewModel
{
    public class TrainerHomeViewModels
    {
        public IEnumerable<Trainer> trainers { get; set; }
        public Trainer trainer { get; set; }
        public int gymid { get; set; }
    }
}
