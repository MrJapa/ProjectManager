using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Migrations
{
    public partial class OnModelCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamWorker",
                table: "TeamWorker");

            migrationBuilder.DropIndex(
                name: "IX_TeamWorker_TeamId",
                table: "TeamWorker");

            migrationBuilder.DropColumn(
                name: "TeamWorkerId",
                table: "TeamWorker");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamWorker",
                table: "TeamWorker",
                columns: new[] { "TeamId", "WorkerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamWorker",
                table: "TeamWorker");

            migrationBuilder.AddColumn<int>(
                name: "TeamWorkerId",
                table: "TeamWorker",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamWorker",
                table: "TeamWorker",
                column: "TeamWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamWorker_TeamId",
                table: "TeamWorker",
                column: "TeamId");
        }
    }
}
