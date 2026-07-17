using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoPress.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class apisorthingshop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "ClosingTime",
                table: "ShopOwnerProfiles",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ShopOwnerProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstimatedDeliveryMinutes",
                table: "ShopOwnerProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "ShopOwnerProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumOrderAmount",
                table: "ShopOwnerProfiles",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "OpeningTime",
                table: "ShopOwnerProfiles",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "ShopImageUrl",
                table: "ShopOwnerProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingTime",
                table: "ShopOwnerProfiles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ShopOwnerProfiles");

            migrationBuilder.DropColumn(
                name: "EstimatedDeliveryMinutes",
                table: "ShopOwnerProfiles");

            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "ShopOwnerProfiles");

            migrationBuilder.DropColumn(
                name: "MinimumOrderAmount",
                table: "ShopOwnerProfiles");

            migrationBuilder.DropColumn(
                name: "OpeningTime",
                table: "ShopOwnerProfiles");

            migrationBuilder.DropColumn(
                name: "ShopImageUrl",
                table: "ShopOwnerProfiles");
        }
    }
}
