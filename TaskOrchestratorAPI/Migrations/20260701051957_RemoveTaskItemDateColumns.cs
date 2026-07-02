using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskOrchestratorAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTaskItemDateColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedOn",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "TaskItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompletedOn",
                table: "TaskItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "TaskItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
