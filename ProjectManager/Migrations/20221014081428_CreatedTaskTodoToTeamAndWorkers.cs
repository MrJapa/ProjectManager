using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Migrations
{
    public partial class CreatedTaskTodoToTeamAndWorkers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentTodoToDoId",
                table: "Worker",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentTaskTaskId",
                table: "Team",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Worker_CurrentTodoToDoId",
                table: "Worker",
                column: "CurrentTodoToDoId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_CurrentTaskTaskId",
                table: "Team",
                column: "CurrentTaskTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Tasks_CurrentTaskTaskId",
                table: "Team",
                column: "CurrentTaskTaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_ToDo_CurrentTodoToDoId",
                table: "Worker",
                column: "CurrentTodoToDoId",
                principalTable: "ToDo",
                principalColumn: "ToDoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Tasks_CurrentTaskTaskId",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_Worker_ToDo_CurrentTodoToDoId",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_Worker_CurrentTodoToDoId",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_Team_CurrentTaskTaskId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "CurrentTodoToDoId",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "CurrentTaskTaskId",
                table: "Team");
        }
    }
}
