using Microsoft.EntityFrameworkCore;
namespace Gym_management_System.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Member> Members { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Gym>().HasData(
                new Gym
                {
                    Id = 1,
                    GymName = "Fitness World",
                    GymAddress = "Kathmandu"
                });
            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    Id = 1,
                    MemberName = "Ram Khatri",
                    Address = "Changunarayan-8-Bhaktapur",
                    Phone = "9876543210",
                    Gender = "Male",
                    city = "Kathmandu",
                    PhotoUrl = "Blank.jpg",
                    GymId = 1
                },
                new Member
                {
                    Id = 2,
                    MemberName = "Ram Hari Khatri",
                    Address = "Changunarayan-7-Bhaktapur",
                    Phone = "9876543211",
                    Gender = "Male",
                    city = "Kathmandu",
                    PhotoUrl = "Blank.jpg",
                    GymId = 1
                });
        }
        public DbSet<Gym> Gyms { get; set; }
    }
}
