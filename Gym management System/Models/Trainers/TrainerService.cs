using Microsoft.AspNetCore.Http.HttpResults;

namespace Gym_management_System.Models.Trainers
{
    public class TrainerService:ITrainerService
    {
        private readonly ApplicationDbContext _context;
        public TrainerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Trainer> GetTrainersByGymId(int gymid)
        {
            return _context.Trainers.
                Where(g=>g.GymId==gymid)
                .ToList();
        }
        public Trainer GetTrainer(int id)
        { 
            return _context.Trainers.Find(id);
        }
        public Trainer AddTrainer(Trainer trainer) 
        { 
            _context.Trainers.Add(trainer);
            _context.SaveChanges();
            return trainer;
        }
        public Trainer Update(Trainer changes)
        {
            var trainer = _context.Trainers.Attach(changes);
            trainer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return changes;
        }
        public Trainer Delete(int id)
        {
            var trainer = _context.Trainers.Find(id);
            if (trainer == null)
            {
                return null;
            }
            _context.Trainers.Remove(trainer);
            _context.SaveChanges();
            return trainer;
        }
    }
}
