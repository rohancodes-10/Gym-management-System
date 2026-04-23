namespace Gym_management_System.Models
{
    public class Trainer
    {
        public string TrainerName{ get; set; }
        public string Phone { get; set; }
        public int? Id { get; set; }
        public int GymId { get; set; }

        public  Gym Gym {  get; set; } 
        public string PhotoUrl { get; set; }

    }
}
