using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoPress.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class orderpricechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClothTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopOwnerClothPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopOwnerId = table.Column<int>(type: "int", nullable: false),
                    ClothTypeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopOwnerClothPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopOwnerClothPrices_ApplicationUsers_ShopOwnerId",
                        column: x => x.ShopOwnerId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopOwnerClothPrices_ClothTypes_ClothTypeId",
                        column: x => x.ClothTypeId,
                        principalTable: "ClothTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothTypes_Name",
                table: "ClothTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShopOwnerClothPrices_ClothTypeId",
                table: "ShopOwnerClothPrices",
                column: "ClothTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopOwnerClothPrices_ShopOwnerId_ClothTypeId",
                table: "ShopOwnerClothPrices",
                columns: new[] { "ShopOwnerId", "ClothTypeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopOwnerClothPrices");

            migrationBuilder.DropTable(
                name: "ClothTypes");
        }
    }
}
