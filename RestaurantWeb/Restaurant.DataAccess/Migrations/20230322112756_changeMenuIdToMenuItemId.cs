using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.DataAccess.Migrations
{
    public partial class changeMenuIdToMenuItemId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_MenuItem_MenuId",
                table: "ShoppingCart");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "ShoppingCart",
                newName: "MenuItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_MenuId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_MenuItem_MenuItemId",
                table: "ShoppingCart",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_MenuItem_MenuItemId",
                table: "ShoppingCart");

            migrationBuilder.RenameColumn(
                name: "MenuItemId",
                table: "ShoppingCart",
                newName: "MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_MenuItemId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_MenuItem_MenuId",
                table: "ShoppingCart",
                column: "MenuId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
