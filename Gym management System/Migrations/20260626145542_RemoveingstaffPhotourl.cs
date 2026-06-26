using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_management_System.Migrations
{
    /// <inheritdoc />
    public partial class RemoveingstaffPhotourl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Staffs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: 10001,
                column: "PhotoUrl",
                value: "Blank.jpg");
        }
    }
}
