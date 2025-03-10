using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class OneToOneMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "TaskDetails",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_TaskId",
                table: "TaskDetails",
                column: "TaskId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDetails_Tasks_TaskId",
                table: "TaskDetails",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDetails_Tasks_TaskId",
                table: "TaskDetails");

            migrationBuilder.DropIndex(
                name: "IX_TaskDetails_TaskId",
                table: "TaskDetails");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "TaskDetails");

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "Cooking", 0 },
                    { 2, "Dishes", 0 }
                });
        }
    }
}
