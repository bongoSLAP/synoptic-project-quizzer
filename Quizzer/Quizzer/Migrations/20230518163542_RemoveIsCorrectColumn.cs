using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzer.Migrations
{
    public partial class RemoveIsCorrectColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Answer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Answer",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
