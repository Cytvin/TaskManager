using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewUsersTasksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TasksUsers",
                table: "TasksUsers");

            migrationBuilder.DropIndex(
                name: "IX_TasksUsers_UserId",
                table: "TasksUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TasksUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TasksUsers",
                table: "TasksUsers",
                columns: new[] { "UserId", "TaskId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TasksUsers",
                table: "TasksUsers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TasksUsers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TasksUsers",
                table: "TasksUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TasksUsers_UserId",
                table: "TasksUsers",
                column: "UserId");
        }
    }
}
