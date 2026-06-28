🏋️ Gym Management System

A full-stack web application built with ASP.NET Core MVC for managing gym operations including members, trainers, staff, and membership payments — with role-based access control and custom authentication built from scratch.


🚀 Features

🔐 Authentication & Authorization


Custom session-based authentication (no ASP.NET Identity)
BCrypt password hashing
AuthFilter for route protection across all roles
Role-based redirects after login (Admin / Staff / Trainer)


👤 Role-Based Access

RoleAccessAdminFull system access — manage members, staff, trainers, paymentsStaffManage members and membership paymentsTrainerView assigned members

👥 Member Management


Add, edit, and view gym members
Assign trainers to members via nullable FK relationship
Track membership status (Active / Expired)


💳 Membership & Payments


Create and manage membership plans
Record payments and track expiry dates
Automatic expiry detection via UpdateExpiredAsync
Blocks new payment if an active membership already exists
returnUrl back-navigation pattern for smooth UX


🏃 Trainer Management


Add and manage trainers
Assign trainers to multiple members (one-to-many)


📊 Dashboard


Stat cards showing total members, active memberships, trainers, and staff



🛠️ Tech Stack

LayerTechnologyFrameworkASP.NET Core MVC (.NET 8)LanguageC#ORMEntity Framework CoreDatabaseSQL ServerAuthCustom Session-Based + BCryptFrontendRazor Views, BootstrapToolsVisual Studio, Git


📁 Project Structure

GymManagementSystem/
├── Controllers/
│   ├── BaseController.cs        # Role-helper methods
│   ├── AccountController.cs     # Login / Logout
│   ├── MemberController.cs
│   ├── TrainerController.cs
│   ├── StaffController.cs
│   └── MembershipPaymentController.cs
├── Models/
│   ├── Member.cs
│   ├── Trainer.cs
│   ├── Staff.cs
│   ├── MembershipPlan.cs
│   └── MembershipPayment.cs
├── Filters/
│   └── AuthFilter.cs            # Session-based route protection
├── Data/
│   └── AppDbContext.cs
├── Views/
│   └── ...                      # Razor views per controller
└── appsettings.json             # Connection string (see setup)


⚙️ Getting Started

Prerequisites


.NET 8 SDK
SQL Server (LocalDB or full)
Visual Studio 2026 or VS Code


Setup


Clone the repository


bash   git clone https://github.com/rohancodes-10/GymManagementSystem.git
   cd GymManagementSystem


Configure the database connection
Open appsettings.json and update the connection string:


json   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=GymDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }


Apply migrations


bash   dotnet ef database update


Run the application


bash   dotnet run


Open your browser at https://localhost:5001



🔑 Demo Credentials

|Role     |Email              |Password 
| Admin   | Owner1@gmail.com  | 12345678   |
| Staff   | staff@gym.com     | Staff@10   |
| Trainer | trainer@gym.com   | Trainer@123 |



📸 Screenshots









📝 Notes


The term "Manager" in the codebase refers to "Staff" — this is an early naming decision that is kept consistent throughout the code.
Authentication is intentionally built from scratch (without ASP.NET Identity) to demonstrate understanding of session management and password hashing fundamentals.



👨‍💻 Author

Rohan KC


📧 rohankc9988@gmail.com
🎓 BIT Student — Bhaktapur Multiple Campus, Tribhuvan University
🌐 GitHub: rohancodes-10



📄 License

This project is open source and available under the MIT License.