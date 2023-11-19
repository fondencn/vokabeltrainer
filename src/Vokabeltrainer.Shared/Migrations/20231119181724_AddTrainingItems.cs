using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VokabelTrainer.Model.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id_TrainingRun = table.Column<int>(type: "INTEGER", nullable: false),
                    Id_Word = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false),
                    TrainingDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingItems_TrainingRuns_Id_TrainingRun",
                        column: x => x.Id_TrainingRun,
                        principalTable: "TrainingRuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingItems_Words_Id_Word",
                        column: x => x.Id_Word,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Words_Id_Lesson",
                table: "Words",
                column: "Id_Lesson");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRuns_Id_Lesson",
                table: "TrainingRuns",
                column: "Id_Lesson");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingItems_Id_TrainingRun",
                table: "TrainingItems",
                column: "Id_TrainingRun");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingItems_Id_Word",
                table: "TrainingItems",
                column: "Id_Word");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingRuns_Lessons_Id_Lesson",
                table: "TrainingRuns",
                column: "Id_Lesson",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Lessons_Id_Lesson",
                table: "Words",
                column: "Id_Lesson",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingRuns_Lessons_Id_Lesson",
                table: "TrainingRuns");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Lessons_Id_Lesson",
                table: "Words");

            migrationBuilder.DropTable(
                name: "TrainingItems");

            migrationBuilder.DropIndex(
                name: "IX_Words_Id_Lesson",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_TrainingRuns_Id_Lesson",
                table: "TrainingRuns");
        }
    }
}
