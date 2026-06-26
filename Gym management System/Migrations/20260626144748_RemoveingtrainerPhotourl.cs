using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_management_System.Migrations
{
    /// <inheritdoc />
    public partial class RemoveingtrainerPhotourl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Trainers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PhotoUrl",
                value: "Blank.jpg");
        }
    }
}
