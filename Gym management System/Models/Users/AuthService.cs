using System.Security.Cryptography;
using System.Diagnostics;
using System.Text;

namespace Gym_management_System.Models.Users
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }
        private string HashPassword(string Pawword)
        {
            using var sha256 = SHA256.Create();
            var bytes=Encoding.UTF8.GetBytes(Pawword);
            var hash=sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        

public User? Login(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        Debug.WriteLine($"=== Searching for: {email}");
        Debug.WriteLine($"=== User found: {user != null}");

        if (user == null) return null;

        string hashedInput = HashPassword(password);
        Debug.WriteLine($"=== Input hash:  {hashedInput}");
        Debug.WriteLine($"=== Stored hash: {user.PasswordHash}");
        Debug.WriteLine($"=== Match: {hashedInput == user.PasswordHash}");

        if (hashedInput != user.PasswordHash) return null;
        return user;
    }
    public User Register(User user, string plainPassword)
        {
            user.PasswordHash = HashPassword(plainPassword);
            user.CreatedAt = DateTime.Now;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public User? GetUserById(int id)
        {
            return _context.Users.Find(id);
        }
    }
}