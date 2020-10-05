using Microsoft.EntityFrameworkCore.Migrations;

namespace HorrorVue.Data.Migrations
{
    public partial class CollectionMovieCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Collections_CollectionId",
                table: "Movies");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Collections_CollectionId",
                table: "Movies",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Collections_CollectionId",
                table: "Movies");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Collections_CollectionId",
                table: "Movies",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
