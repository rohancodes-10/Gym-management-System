using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_management_System.Migrations
{
    /// <inheritdoc />
    public partial class adding_data_to_Gym : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "City", "GymAddress", "Phone" },
                values: new object[] { "kathmandu", "Gokarneshwor-8-Kathmandu", "9876543210" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "City", "GymAddress", "Phone" },
                values: new object[] { "", "Kathmandu", "" });
        }
    }
}
