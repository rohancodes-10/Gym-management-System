using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Gym_management_System.Models.Gyms
{
    public class GymService : IGymService
    {
        private readonly ApplicationDbContext context;
        public GymService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Gym> GetAllGyms()
        {
            return context.Gyms.ToList();
        }
        public Gym GetGym(int id)
        {
            return context.Gyms.Find(id);
        }
        public Gym AddGym(Gym gym)
        {
            context.Gyms.Add(gym);
            context.SaveChanges();
            return gym;
        }
        public Gym Update(Gym changes) 
        {
            var gym = context.Gyms.Attach(changes);
            gym.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changes;
        }
        public Gym Delete(int id) 
        {
           var gym= context.Gyms.Find(id);
            if (gym == null)
            {
                return null;
            }
            context .Gyms.Remove(gym);
            return gym;
        }

    }
}
