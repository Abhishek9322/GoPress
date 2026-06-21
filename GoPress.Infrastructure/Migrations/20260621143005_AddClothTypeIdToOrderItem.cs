using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoPress.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClothTypeIdToOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClothTypeId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ClothTypeId",
                table: "OrderItems",
                column: "ClothTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ClothTypes_ClothTypeId",
                table: "OrderItems",
                column: "ClothTypeId",
                principalTable: "ClothTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ClothTypes_ClothTypeId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ClothTypeId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ClothTypeId",
                table: "OrderItems");
        }
    }
}
