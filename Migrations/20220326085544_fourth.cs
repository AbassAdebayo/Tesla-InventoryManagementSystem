using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagemenSystem_Ims.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnGoods_SalesItems_SalesItemId",
                table: "ReturnGoods");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesItems_Items_ItemId",
                table: "SalesItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesItems_ItemId",
                table: "SalesItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "SalesItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SalesItems");

            migrationBuilder.DropColumn(
                name: "CustomerEmailAddress",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "PricePerUnit",
                table: "SalesItems",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Sales",
                newName: "PricePerUnit");

            migrationBuilder.RenameColumn(
                name: "SalesItemId",
                table: "ReturnGoods",
                newName: "SalesId");

            migrationBuilder.RenameIndex(
                name: "IX_ReturnGoods_SalesItemId",
                table: "ReturnGoods",
                newName: "IX_ReturnGoods_SalesId");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ItemId",
                table: "Sales",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnGoods_Sales_SalesId",
                table: "ReturnGoods",
                column: "SalesId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Items_ItemId",
                table: "Sales",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnGoods_Sales_SalesId",
                table: "ReturnGoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Items_ItemId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ItemId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "SalesItems",
                newName: "PricePerUnit");

            migrationBuilder.RenameColumn(
                name: "PricePerUnit",
                table: "Sales",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "SalesId",
                table: "ReturnGoods",
                newName: "SalesItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ReturnGoods_SalesId",
                table: "ReturnGoods",
                newName: "IX_ReturnGoods_SalesItemId");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "SalesItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "SalesItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmailAddress",
                table: "Sales",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesItems_ItemId",
                table: "SalesItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnGoods_SalesItems_SalesItemId",
                table: "ReturnGoods",
                column: "SalesItemId",
                principalTable: "SalesItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesItems_Items_ItemId",
                table: "SalesItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
