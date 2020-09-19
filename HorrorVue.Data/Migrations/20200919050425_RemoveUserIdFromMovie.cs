using Microsoft.EntityFrameworkCore.Migrations;

namespace HorrorVue.Data.Migrations
{
    public partial class RemoveUserIdFromMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Movies_MovieId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_MovieId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "AppUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "AppUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_MovieId",
                table: "AppUsers",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Movies_MovieId",
                table: "AppUsers",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
