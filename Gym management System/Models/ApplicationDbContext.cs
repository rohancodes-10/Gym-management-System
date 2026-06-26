using Microsoft.EntityFrameworkCore;
using Gym_management_System.Models.Members;
using Gym_management_System.Models.Trainers;
using Gym_management_System.Models.Gyms;
using Gym_management_System.Models.Staffs;
using Gym_management_System.Models.Users;
using Gym_management_System.Models.MembershipPlans;
using Gym_management_System.Models.MembershipPayments;
namespace Gym_management_System.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MembershipPlan> MembershipPlans{get; set; }
        public DbSet<MembershipPayment> MembershipPayments{get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
           

            modelBuilder.Entity<MembershipPayment>()
                .HasOne(mp => mp.MembershipPlan)
                .WithMany()
                .HasForeignKey(mp => mp.MembershipPlanId)
                .OnDelete(DeleteBehavior.Restrict); 
        

        modelBuilder.Entity<Gym>().Property(e=>e.Id).UseIdentityColumn(1001,1);
            modelBuilder.Entity<Member>().Property(e => e.Id).UseIdentityColumn(101, 1);
            modelBuilder.Entity<Trainer>().Property(e => e.Id).UseIdentityColumn(1, 1);
            modelBuilder.Entity<Staff>().Property(e => e.Id).UseIdentityColumn(10001, 1);

            modelBuilder.Entity<Staff>().HasData(
              new Staff
              {
                  Id = 10001,
                  StaffName = "HariLal",
                  StaffAddress = "Gokarneshwor-8-Kathmandu",
                  Phone = "9876543210",
                  Age = 29,
                  Gender="Male",
                  GymId = 1001,
                  PhotoUrl = "Blank.jpg"

              });
            modelBuilder.Entity<Trainer>().HasData(
               new Trainer
               {
                   Id = 1,
                   TrainerName = "HariLal",
                   TrainerAddress = "Gokarneshwor-8-Kathmandu",
                   Phone = "9876543210",
                   Age=29,
                   GymId = 1001,
                 
                   
               });


            modelBuilder.Entity<Gym>().HasData(
                new Gym
                {
                    Id = 1001,
                    GymName = "Fitness World",
                    GymAddress = "Gokarneshwor-8-Kathmandu",
                    Phone="9876543210",
                    City="kathmandu"
                });
           

            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    Id = 101,
                    MemberName = "Ram Khatri",
                    Address = "Changunarayan-8-Bhaktapur",
                    Phone = "9876543210",
                    Gender = "Male",
                    city = "Kathmandu",
                  

                    GymId = 1001

                 

                },
                new Member
                {
                    Id = 102,
                    MemberName = "Ram Hari Khatri",
                    Address = "Changunarayan-7-Bhaktapur",
                    Phone = "9876543211",
                    Gender = "Male",
                    city = "Kathmandu",
                   

                    GymId = 1001
                });
        }
       

                
               
        
      

    }
}
