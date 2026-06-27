using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoPress.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addnewrejectuserprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedAt",
                table: "ApplicationUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectedAt",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "ApplicationUsers");
        }
    }
}
