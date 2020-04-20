using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTest.DbAccess.Migrations
{
    public partial class TestCourceRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TestCourses_TestId",
                table: "TestCourses",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestCourses_Courses_CourseId",
                table: "TestCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestCourses_Tests_TestId",
                table: "TestCourses",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestCourses_Courses_CourseId",
                table: "TestCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_TestCourses_Tests_TestId",
                table: "TestCourses");

            migrationBuilder.DropIndex(
                name: "IX_TestCourses_TestId",
                table: "TestCourses");
        }
    }
}
