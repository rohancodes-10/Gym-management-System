namespace Gym_management_System.Models.Trainers
{
    public class TrainerService:ITrainerService
    {
        private ApplicationDbContext _context;
        public TrainerService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IEnumerable<Trainer> GetTrainers()
        {
            return _context.Trainers.ToList();
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
    }
}
