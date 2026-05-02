using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_management_System.Migrations
{
    /// <inheritdoc />
    public partial class Inserting_data_trainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "Age", "GymId", "Phone", "PhotoUrl", "TrainerAddress", "TrainerName" },
                values: new object[] { 1, 29, 1001, "9876543210", "Blank.jpg", "Gokarneshwor-8-Kathmandu", "HariLal" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
