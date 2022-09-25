using Microsoft.EntityFrameworkCore.Migrations;

namespace SHA_QrAttendanceSystem.Migrations
{
    public partial class AddStuIdcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StuID",
                table: "Students",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StuID",
                table: "Students");
        }
    }
}
