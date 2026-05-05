namespace Gym_management_System.Models.Users
{
    public interface IAuthService
    {
        public User? Login(string username, string password);
        public User Register(User user, string plainpassword);
        public User? GetUserById(int id);
    }
}
