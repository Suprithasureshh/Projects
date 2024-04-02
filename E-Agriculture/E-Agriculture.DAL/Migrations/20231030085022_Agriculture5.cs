using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Agriculture.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Agriculture5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "answer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "User_Name",
                table: "answer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "answer");

            migrationBuilder.DropColumn(
                name: "User_Name",
                table: "answer");
        }
    }
}
