using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class QuestionMessageFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuestionMessage_QuestionId",
                table: "QuestionMessage");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "QuestionMessage",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionMessage_QuestionId",
                table: "QuestionMessage",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuestionMessage_QuestionId",
                table: "QuestionMessage");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "QuestionMessage",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionMessage_QuestionId",
                table: "QuestionMessage",
                column: "QuestionId",
                unique: true);
        }
    }
}
