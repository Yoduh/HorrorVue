using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HorrorVue.Data.Migrations
{
    public partial class Ratings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<int>>(
                name: "Ratings",
                table: "Rankings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ratings",
                table: "Rankings");
        }
    }
}
