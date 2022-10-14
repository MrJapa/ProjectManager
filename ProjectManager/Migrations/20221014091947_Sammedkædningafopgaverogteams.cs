using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Migrations
{
    public partial class Sammedkædningafopgaverogteams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "ToDo",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Tasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDo_WorkerId",
                table: "ToDo",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TeamId",
                table: "Tasks",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Team_TeamId",
                table: "Tasks",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDo_Worker_WorkerId",
                table: "ToDo",
                column: "WorkerId",
                principalTable: "Worker",
                principalColumn: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Team_TeamId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDo_Worker_WorkerId",
                table: "ToDo");

            migrationBuilder.DropIndex(
                name: "IX_ToDo_WorkerId",
                table: "ToDo");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TeamId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "ToDo");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Tasks");
        }
    }
}
