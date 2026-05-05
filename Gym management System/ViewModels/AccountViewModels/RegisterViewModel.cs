namespace Gym_management_System.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? GymId { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; }

    }
}
