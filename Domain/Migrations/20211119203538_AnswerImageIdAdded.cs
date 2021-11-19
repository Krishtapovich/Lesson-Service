using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AnswerImageIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Answers_AnswerId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AnswerId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "CloudId",
                table: "Images",
                newName: "FileName");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Answers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ImageId",
                table: "Answers",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Images_ImageId",
                table: "Answers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Images_ImageId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_ImageId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Images",
                newName: "CloudId");

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "Images",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AnswerId",
                table: "Images",
                column: "AnswerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Answers_AnswerId",
                table: "Images",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
