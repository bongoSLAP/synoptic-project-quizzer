using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzer.Migrations
{
    public partial class RenameIndexColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Index",
                table: "Question",
                newName: "QuestionIndex");

            migrationBuilder.RenameColumn(
                name: "Index",
                table: "Answer",
                newName: "AnswerIndex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestionIndex",
                table: "Question",
                newName: "Index");

            migrationBuilder.RenameColumn(
                name: "AnswerIndex",
                table: "Answer",
                newName: "Index");
        }
    }
}
