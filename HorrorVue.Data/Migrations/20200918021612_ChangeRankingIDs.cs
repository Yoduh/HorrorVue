using Microsoft.EntityFrameworkCore.Migrations;

namespace HorrorVue.Data.Migrations
{
    public partial class ChangeRankingIDs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rankings_Collections_CollectionId",
                table: "Rankings");

            migrationBuilder.DropForeignKey(
                name: "FK_Rankings_AppUsers_UserId",
                table: "Rankings");

            migrationBuilder.DropIndex(
                name: "IX_Rankings_UserId",
                table: "Rankings");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Rankings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Rankings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rankings_Collections_CollectionId",
                table: "Rankings",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rankings_Collections_CollectionId",
                table: "Rankings");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Rankings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "Rankings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Rankings_UserId",
                table: "Rankings",
                column: "UserId");

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
    }
}
