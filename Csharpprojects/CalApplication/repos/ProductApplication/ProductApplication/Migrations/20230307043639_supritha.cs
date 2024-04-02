using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApplication.Migrations
{
    /// <inheritdoc />
    public partial class supritha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductTable",
                newName: "Product_Name");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "ProductTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Product_Name",
                table: "ProductTable",
                newName: "Name");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "ProductTable",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
