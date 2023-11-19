using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VokabelTrainer.Model.Migrations
{
    /// <inheritdoc />
    public partial class FixWordLessonRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_Lesson",
                table: "Words",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_Lesson",
                table: "Words");
        }
    }
}
