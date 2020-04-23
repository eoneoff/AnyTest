using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTest.DbAccess.Migrations
{
    public partial class AnswerPercentTypeAndOrderNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Percent",
                table: "TestAnswers",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "OrderNo",
                table: "TestAnswers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNo",
                table: "TestAnswers");

            migrationBuilder.AlterColumn<long>(
                name: "Percent",
                table: "TestAnswers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
