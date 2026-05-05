using Gym_management_System.Models;
using Gym_management_System.Models.Gyms;
using Gym_management_System.Models.Trainers;
using Gym_management_System.Models.Members;
using Gym_management_System.Models.Staffs;
using Gym_management_System.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Gym_management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<IGymService, GymService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddSession();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        )
            ));

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
            app.UseSession();
            app.UseAuthorization();

            app.MapStaticAssets();
          
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Gym}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
