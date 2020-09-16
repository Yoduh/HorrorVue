using Microsoft.EntityFrameworkCore.Migrations;

namespace HorrorVue.Data.Migrations
{
    public partial class InitialFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ranking_Collections_CollectionId",
                table: "Ranking");

            migrationBuilder.DropForeignKey(
                name: "FK_Ranking_AppUsers_UserId",
                table: "Ranking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ranking",
                table: "Ranking");

            migrationBuilder.RenameTable(
                name: "Ranking",
                newName: "Rankings");

            migrationBuilder.RenameIndex(
                name: "IX_Ranking_UserId",
                table: "Rankings",
                newName: "IX_Rankings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ranking_CollectionId",
                table: "Rankings",
                newName: "IX_Rankings_CollectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rankings",
                table: "Rankings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rankings_Collections_CollectionId",
                table: "Rankings",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rankings_AppUsers_UserId",
                table: "Rankings",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rankings_Collections_CollectionId",
                table: "Rankings");

            migrationBuilder.DropForeignKey(
                name: "FK_Rankings_AppUsers_UserId",
                table: "Rankings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rankings",
                table: "Rankings");

            migrationBuilder.RenameTable(
                name: "Rankings",
                newName: "Ranking");

            migrationBuilder.RenameIndex(
                name: "IX_Rankings_UserId",
                table: "Ranking",
                newName: "IX_Ranking_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rankings_CollectionId",
                table: "Ranking",
                newName: "IX_Ranking_CollectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ranking",
                table: "Ranking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ranking_Collections_CollectionId",
                table: "Ranking",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ranking_AppUsers_UserId",
                table: "Ranking",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
