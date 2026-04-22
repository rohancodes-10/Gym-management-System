using Microsoft.EntityFrameworkCore;
namespace Gym_management_System.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    Id = 101,
                    MemberName = "Ram Khatri",
                    Address = "Changunarayan-8-Bhaktapur",
                    Phone = "9876543210",
                    Gender = "Male",
                    city = "Kathmandu",
                    PhotoUrl = "Blank.jpg"
                },
                new Member
                {
                    Id = 102,
                    MemberName = "Ram Hari Khatri",
                    Address = "Changunarayan-7-Bhaktapur",
                    Phone = "9876543211",
                    Gender = "Male",
                    city = "Kathmandu",
                    PhotoUrl = "Blank.jpg"
                });
        }
        //public DbSet<Gym> Gyms { get; set; }
    }
}
