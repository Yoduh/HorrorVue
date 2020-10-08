using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HorrorVue.Data.Migrations
{
    public partial class Invites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    FromUserId = table.Column<int>(nullable: false),
                    ToUserId = table.Column<int>(nullable: false),
                    CollectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invite_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invite_AppUsers_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invite_AppUsers_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invite_CollectionId",
                table: "Invite",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Invite_FromUserId",
                table: "Invite",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invite_ToUserId",
                table: "Invite",
                column: "ToUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invite");
        }
    }
}
