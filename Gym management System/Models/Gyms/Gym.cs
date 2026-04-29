using Gym_management_System.Models.Members;
namespace Gym_management_System.Models.Gyms
{
    public class Gym
    {
        public int Id { get; set; }
        public string GymName { get; set; } = string.Empty;
        public string GymAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Phone {  get; set; } = string.Empty;
        public ICollection<Member> members { get; set; }
        
    }
}
