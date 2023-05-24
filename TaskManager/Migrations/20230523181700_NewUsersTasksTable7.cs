using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewUsersTasksTable7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_Tasks_Id",
                table: "TaskAssignments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TaskAssignments",
                newName: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_Tasks_TaskId",
                table: "TaskAssignments",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_Tasks_TaskId",
                table: "TaskAssignments");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "TaskAssignments",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_Tasks_Id",
                table: "TaskAssignments",
                column: "Id",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
