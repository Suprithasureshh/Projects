using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeInformation.Migrations
{
    /// <inheritdoc />
    public partial class vgt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Employee",
                newName: "Languages");

            migrationBuilder.RenameColumn(
                name: "ConfirmPassword",
                table: "Employee",
                newName: "Experience");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Languages",
                table: "Employee",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Experience",
                table: "Employee",
                newName: "ConfirmPassword");
        }
    }
}
