using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTest.DbAccess.Migrations
{
    public partial class AddCourseToPrecondition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Preconditions",
                table: "Preconditions");

            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "Preconditions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Preconditions",
                table: "Preconditions",
                columns: new[] { "TestId", "CourseId", "PreconditionId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Preconditions",
                table: "Preconditions");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Preconditions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Preconditions",
                table: "Preconditions",
                columns: new[] { "TestId", "PreconditionId" });
        }
    }
}
