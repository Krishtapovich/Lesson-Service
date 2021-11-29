using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class QuestionFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuestionMessage_QuestionId",
                table: "QuestionMessage");

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
                table: "QuestionMessage",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionMessage_QuestionId",
                table: "QuestionMessage",
                column: "QuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionMessage_StudentId",
                table: "QuestionMessage",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionMessage_Student_StudentId",
                table: "QuestionMessage",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionMessage_Student_StudentId",
                table: "QuestionMessage");

            migrationBuilder.DropIndex(
                name: "IX_QuestionMessage_QuestionId",
                table: "QuestionMessage");

            migrationBuilder.DropIndex(
                name: "IX_QuestionMessage_StudentId",
                table: "QuestionMessage");

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
                table: "QuestionMessage",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionMessage_QuestionId",
                table: "QuestionMessage",
                column: "QuestionId");
        }
    }
}
