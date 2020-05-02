using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTest.DbAccess.Migrations
{
    public partial class AddedTestSubjectsCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSubject_Subjects_SubjectId",
                table: "TestSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSubject_Tests_TestId",
                table: "TestSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSubject",
                table: "TestSubject");

            migrationBuilder.RenameTable(
                name: "TestSubject",
                newName: "TestSubjects");

            migrationBuilder.RenameIndex(
                name: "IX_TestSubject_SubjectId",
                table: "TestSubjects",
                newName: "IX_TestSubjects_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSubjects",
                table: "TestSubjects",
                columns: new[] { "TestId", "SubjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TestSubjects_Subjects_SubjectId",
                table: "TestSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSubjects_Tests_TestId",
                table: "TestSubjects",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSubjects_Subjects_SubjectId",
                table: "TestSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSubjects_Tests_TestId",
                table: "TestSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSubjects",
                table: "TestSubjects");

            migrationBuilder.RenameTable(
                name: "TestSubjects",
                newName: "TestSubject");

            migrationBuilder.RenameIndex(
                name: "IX_TestSubjects_SubjectId",
                table: "TestSubject",
                newName: "IX_TestSubject_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSubject",
                table: "TestSubject",
                columns: new[] { "TestId", "SubjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TestSubject_Subjects_SubjectId",
                table: "TestSubject",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSubject_Tests_TestId",
                table: "TestSubject",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");
        }
    }
}
