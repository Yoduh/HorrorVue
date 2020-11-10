using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HorrorVue.Data.Migrations
{
    public partial class RatingsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<double>>(
                name: "Ratings",
                table: "Rankings",
                nullable: true,
                oldClrType: typeof(List<int>),
                oldType: "integer[]",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<int>>(
                name: "Ratings",
                table: "Rankings",
                type: "integer[]",
                nullable: true,
                oldClrType: typeof(List<double>),
                oldNullable: true);
        }
    }
}
