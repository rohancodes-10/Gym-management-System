namespace Gym_management_System.Models.Trainers
{
    public interface ITrainerService
    {
         IEnumerable<Trainer> GetTrainersByGymId(int gymid);
         Trainer? GetTrainer(int id);
         Trainer AddTrainer(Trainer trainer);
        Trainer Update(Trainer Changes);
    }
}
