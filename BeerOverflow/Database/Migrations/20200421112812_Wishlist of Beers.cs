using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class WishlistofBeers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrankLists_Beers_BeerID",
                table: "DrankLists");

            migrationBuilder.DropForeignKey(
                name: "FK_DrankLists_Users_UserID",
                table: "DrankLists");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Beers_BeerID",
                table: "WishLists");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Users_UserID",
                table: "WishLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishLists",
                table: "WishLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrankLists",
                table: "DrankLists");

            migrationBuilder.RenameTable(
                name: "WishLists",
                newName: "WishList");

            migrationBuilder.RenameTable(
                name: "DrankLists",
                newName: "DrankList");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_UserID",
                table: "WishList",
                newName: "IX_WishList_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_DrankLists_UserID",
                table: "DrankList",
                newName: "IX_DrankList_UserID");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Beers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID1",
                table: "Beers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishList",
                table: "WishList",
                columns: new[] { "BeerID", "UserID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrankList",
                table: "DrankList",
                columns: new[] { "BeerID", "UserID" });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_UserID",
                table: "Beers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Beers_UserID1",
                table: "Beers",
                column: "UserID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Users_UserID",
                table: "Beers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Users_UserID1",
                table: "Beers",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DrankList_Beers_BeerID",
                table: "DrankList",
                column: "BeerID",
                principalTable: "Beers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrankList_Users_UserID",
                table: "DrankList",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Beers_BeerID",
                table: "WishList",
                column: "BeerID",
                principalTable: "Beers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Users_UserID",
                table: "WishList",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Users_UserID",
                table: "Beers");

            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Users_UserID1",
                table: "Beers");

            migrationBuilder.DropForeignKey(
                name: "FK_DrankList_Beers_BeerID",
                table: "DrankList");

            migrationBuilder.DropForeignKey(
                name: "FK_DrankList_Users_UserID",
                table: "DrankList");

            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Beers_BeerID",
                table: "WishList");

            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Users_UserID",
                table: "WishList");

            migrationBuilder.DropIndex(
                name: "IX_Beers_UserID",
                table: "Beers");

            migrationBuilder.DropIndex(
                name: "IX_Beers_UserID1",
                table: "Beers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishList",
                table: "WishList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrankList",
                table: "DrankList");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "Beers");

            migrationBuilder.RenameTable(
                name: "WishList",
                newName: "WishLists");

            migrationBuilder.RenameTable(
                name: "DrankList",
                newName: "DrankLists");

            migrationBuilder.RenameIndex(
                name: "IX_WishList_UserID",
                table: "WishLists",
                newName: "IX_WishLists_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_DrankList_UserID",
                table: "DrankLists",
                newName: "IX_DrankLists_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishLists",
                table: "WishLists",
                columns: new[] { "BeerID", "UserID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrankLists",
                table: "DrankLists",
                columns: new[] { "BeerID", "UserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_DrankLists_Beers_BeerID",
                table: "DrankLists",
                column: "BeerID",
                principalTable: "Beers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrankLists_Users_UserID",
                table: "DrankLists",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Beers_BeerID",
                table: "WishLists",
                column: "BeerID",
                principalTable: "Beers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Users_UserID",
                table: "WishLists",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
