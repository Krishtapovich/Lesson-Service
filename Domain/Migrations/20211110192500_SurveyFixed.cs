using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class SurveyFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupNumber",
                table: "Surveys");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GroupNumber",
                table: "Surveys",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
