namespace Gym_management_System.Models.Users
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Id { get; set; }
        public int? GymId { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
