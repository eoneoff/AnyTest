using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTest.DbAccess.Migrations
{
    public partial class SmallCorrectionsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Partronimic",
                table: "People");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "People",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Patronimic",
                table: "People",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_Email",
                table: "People",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_Email",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Patronimic",
                table: "People");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Partronimic",
                table: "People",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
