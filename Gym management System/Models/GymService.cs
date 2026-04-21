//using Microsoft.EntityFrameworkCore.Metadata.Conventions;

//namespace Gym_management_System.Models
//{
//    public class GymService:IGymService
//    {
//        private readonly ApplicationDbContext context;
//        public GymService(ApplicationDbContext context)
//        {
//            this.context = context;
//        }
//        public IEnumerable<Gym> GetAllGyms()
//        {
//            return context.Gyms.ToList();
//        }
//        public Gym GetGym(int id)
//        {
//            return context.Gyms.Find(id);
//        }
//        public Gym AddGym(Gym gym)
//        {
//           context.Gyms.Add(gym);
//            context.SaveChanges();
//            return gym;
//        }
//    }
//}
