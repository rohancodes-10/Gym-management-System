using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gym_management_System.Migrations
{
    /// <inheritdoc />
    public partial class inserting_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Address", "Gender", "MemberName", "Phone", "PhotoUrl", "city" },
                values: new object[,]
                {
                    { 1, "Changunarayan-8-Bhaktapur", "Male", "Ram Khatri", "9876543210", "Blank.jpg", "Kathmandu" },
                    { 2, "Changunarayan-7-Bhaktapur", "Male", "Ram Hari Khatri", "9876543211", "Blank.jpg", "Kathmandu" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
