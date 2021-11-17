using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class ImageFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Images",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "Images",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CloudId",
                table: "Images",
                type: "TEXT",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CloudId",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Images",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Answers",
                type: "TEXT",
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
    }
}
