using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class RelationsFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionMessage_Questions_QuestionId",
                table: "QuestionMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionMessage",
                table: "QuestionMessage");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Answers");

            migrationBuilder.RenameTable(
                name: "QuestionMessage",
                newName: "QuestionMessages");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Answers",
                newName: "QuestionMessageId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionMessage_QuestionId",
                table: "QuestionMessages",
                newName: "IX_QuestionMessages_QuestionId");

            migrationBuilder.AlterColumn<long>(
                name: "MessageId",
                table: "QuestionMessages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "QuestionMessages",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionMessages",
                table: "QuestionMessages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionMessageId",
                table: "Answers",
                column: "QuestionMessageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_QuestionMessages_QuestionMessageId",
                table: "Answers",
                column: "QuestionMessageId",
                principalTable: "QuestionMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionMessages_Questions_QuestionId",
                table: "QuestionMessages",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_QuestionMessages_QuestionMessageId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionMessages_Questions_QuestionId",
                table: "QuestionMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionMessageId",
                table: "Answers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionMessages",
                table: "QuestionMessages");

            migrationBuilder.RenameTable(
                name: "QuestionMessages",
                newName: "QuestionMessage");

            migrationBuilder.RenameColumn(
                name: "QuestionMessageId",
                table: "Answers",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionMessages_QuestionId",
                table: "QuestionMessage",
                newName: "IX_QuestionMessage_QuestionId");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Answers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "MessageId",
                table: "QuestionMessage",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "QuestionMessage",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionMessage",
                table: "QuestionMessage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionMessage_Questions_QuestionId",
                table: "QuestionMessage",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
