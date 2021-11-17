using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class QuestionMessagePollIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PollId",
                table: "QuestionMessages",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PollId",
                table: "QuestionMessages");
        }
    }
}
