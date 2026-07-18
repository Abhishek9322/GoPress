using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoPress.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addfuctionattimeoflogginisopen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginAt",
                table: "ShopOwnerProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogoutAt",
                table: "ShopOwnerProfiles",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginAt",
                table: "ShopOwnerProfiles");

            migrationBuilder.DropColumn(
                name: "LastLogoutAt",
                table: "ShopOwnerProfiles");
        }
    }
}
