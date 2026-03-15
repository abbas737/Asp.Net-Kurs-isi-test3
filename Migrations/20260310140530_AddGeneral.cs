using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tank_Wiki.Migrations
{
    /// <inheritdoc />
    public partial class AddGeneral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Generals");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Generals",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Generals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeathDate",
                table: "Generals",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Generals");

            migrationBuilder.DropColumn(
                name: "DeathDate",
                table: "Generals");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Generals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Generals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
