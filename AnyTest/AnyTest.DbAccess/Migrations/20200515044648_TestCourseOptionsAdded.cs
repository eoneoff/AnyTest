using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTest.DbAccess.Migrations
{
    public partial class TestCourseOptionsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassScore",
                table: "TestCourses",
                nullable: false,
                defaultValue: 50);

            migrationBuilder.AddColumn<bool>(
                name: "Restricted",
                table: "TestCourses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TriesPermitted",
                table: "TestCourses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassScore",
                table: "TestCourses");

            migrationBuilder.DropColumn(
                name: "Restricted",
                table: "TestCourses");

            migrationBuilder.DropColumn(
                name: "TriesPermitted",
                table: "TestCourses");
        }
    }
}
