using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class Adding_Beer_Rating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Beers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BeerUserRatings",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    BeerID = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerUserRatings", x => new { x.UserID, x.BeerID });
                    table.ForeignKey(
                        name: "FK_BeerUserRatings_Beers_BeerID",
                        column: x => x.BeerID,
                        principalTable: "Beers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeerUserRatings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeerUserRatings_BeerID",
                table: "BeerUserRatings",
                column: "BeerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeerUserRatings");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Beers");
        }
    }
}
