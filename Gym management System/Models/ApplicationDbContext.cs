using Microsoft.EntityFrameworkCore;
namespace Gym_management_System.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Members> Members { get; set; }
    }
}
