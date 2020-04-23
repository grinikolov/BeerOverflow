using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class RatingFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Beers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Beers",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
