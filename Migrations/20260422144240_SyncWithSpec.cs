using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoStudio.Migrations
{
    /// <inheritdoc />
    public partial class SyncWithSpec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Clients",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Clients");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Clients",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
