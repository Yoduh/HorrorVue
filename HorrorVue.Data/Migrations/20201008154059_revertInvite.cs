using Microsoft.EntityFrameworkCore.Migrations;

namespace HorrorVue.Data.Migrations
{
    public partial class revertInvite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InviteCollection");

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Invite",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invite_CollectionId",
                table: "Invite",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invite_Collections_CollectionId",
                table: "Invite",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invite_Collections_CollectionId",
                table: "Invite");

            migrationBuilder.DropIndex(
                name: "IX_Invite_CollectionId",
                table: "Invite");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Invite");

            migrationBuilder.CreateTable(
                name: "InviteCollection",
                columns: table => new
                {
                    InviteId = table.Column<int>(type: "integer", nullable: false),
                    CollectionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InviteCollection", x => new { x.InviteId, x.CollectionId });
                    table.ForeignKey(
                        name: "FK_InviteCollection_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InviteCollection_Invite_InviteId",
                        column: x => x.InviteId,
                        principalTable: "Invite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InviteCollection_CollectionId",
                table: "InviteCollection",
                column: "CollectionId");
        }
    }
}
