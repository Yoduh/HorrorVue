using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HorrorVue.Data.Migrations
{
    public partial class InitialFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Ranking_RankingId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_RankingId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "RankingId",
                table: "Movies");

            migrationBuilder.AddColumn<List<int>>(
                name: "Order",
                table: "Ranking",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Ranking");

            migrationBuilder.AddColumn<int>(
                name: "RankingId",
                table: "Movies",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_RankingId",
                table: "Movies",
                column: "RankingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Ranking_RankingId",
                table: "Movies",
                column: "RankingId",
                principalTable: "Ranking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
