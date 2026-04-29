using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoStudio.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToPhotographer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "HireDate",
                table: "Employees",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Photographers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Employees",
                newName: "HireDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
