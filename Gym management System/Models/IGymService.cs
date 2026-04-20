namespace Gym_management_System.Models
{
    public interface IGymService
    {
        public IEnumerable<Gym> GetAllGyms();
        public Gym GetGym(int id);
        public Gym AddGym(Gym gym);

    }
}
