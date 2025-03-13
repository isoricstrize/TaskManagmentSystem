using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehaviorTaskDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDetails_Tasks_TaskId",
                table: "TaskDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDetails_Tasks_TaskId",
                table: "TaskDetails",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDetails_Tasks_TaskId",
                table: "TaskDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDetails_Tasks_TaskId",
                table: "TaskDetails",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
