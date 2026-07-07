using Gym_management_System.Models;
using Gym_management_System.Models.Gyms;
using Gym_management_System.Models.Trainers;
using Gym_management_System.Models.Members;
using Gym_management_System.Models.Staffs;
using Gym_management_System.Models.Users;
using Gym_management_System.Models.MembershipPlans;
using Gym_management_System.Models.MembershipPayments;
using Microsoft.EntityFrameworkCore;
using Gym_management_System.Models.Complaints;

namespace Gym_management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IMembershipPlansService, MembershipPlansService>();
            
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<IGymService, GymService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<IMembershipPaymentService,MemberPaymentServicecs>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            //builder.Services.AddScoped<IComplaintService, ComplaintService>();
            builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        )
            ));
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
           
            app.UseAuthorization();
            app.UseSession();
            app.MapStaticAssets();
          
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
