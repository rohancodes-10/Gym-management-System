namespace Gym_management_System.Models.Gyms
{
    public interface IGymService
    {
        public IEnumerable<Gym> GetAllGyms();
        public IEnumerable<Gym> GetAllGymswithDetails();
        public Gym GetGym(int id);
        public Gym AddGym(Gym gym);
        public Gym Update(Gym changes);
        public Gym Delete(int id);
    }
}
