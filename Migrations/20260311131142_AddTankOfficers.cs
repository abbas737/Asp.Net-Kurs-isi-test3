using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tank_Wiki.Migrations
{
    /// <inheritdoc />
    public partial class AddTankOfficers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "TankOfficers");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "TankOfficers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeathDate",
                table: "TankOfficers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "TankOfficers");

            migrationBuilder.DropColumn(
                name: "DeathDate",
                table: "TankOfficers");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "TankOfficers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
