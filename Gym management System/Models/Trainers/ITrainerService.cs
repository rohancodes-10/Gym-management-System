namespace Gym_management_System.Models.Trainers
{
    public interface ITrainerService
    {
         IEnumerable<Trainer> GetTrainers();
         Trainer? GetTrainer(int id);
         Trainer AddTrainer(Trainer trainer);
        Trainer Update(Trainer Changes);
    }
}
