using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class CustomIdentityRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Beers_BeerID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BeerID_UserID",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "BeerID",
                table: "Reviews",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BeerID_UserID",
                table: "Reviews",
                columns: new[] { "BeerID", "UserID" },
                unique: true,
                filter: "[BeerID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Beers_BeerID",
                table: "Reviews",
                column: "BeerID",
                principalTable: "Beers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Beers_BeerID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BeerID_UserID",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "BeerID",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BeerID_UserID",
                table: "Reviews",
                columns: new[] { "BeerID", "UserID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Beers_BeerID",
                table: "Reviews",
                column: "BeerID",
                principalTable: "Beers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
