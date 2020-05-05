using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTest.DbAccess.Migrations
{
    public partial class AnswersToQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestPasses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<long>(nullable: false),
                    StudentId = table.Column<long>(nullable: false),
                    PassedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestPasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestPasses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestPasses_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnswerPasses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassId = table.Column<long>(nullable: false),
                    QuestionId = table.Column<long>(nullable: false),
                    AnswerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerPasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerPasses_TestAnswers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "TestAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerPasses_TestPasses_PassId",
                        column: x => x.PassId,
                        principalTable: "TestPasses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnswerPasses_TestQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "TestQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerPasses_AnswerId",
                table: "AnswerPasses",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerPasses_PassId",
                table: "AnswerPasses",
                column: "PassId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerPasses_QuestionId",
                table: "AnswerPasses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestPasses_StudentId",
                table: "TestPasses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TestPasses_TestId",
                table: "TestPasses",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerPasses");

            migrationBuilder.DropTable(
                name: "TestPasses");
        }
    }
}
