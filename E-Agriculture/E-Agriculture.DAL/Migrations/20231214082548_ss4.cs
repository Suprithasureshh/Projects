using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Agriculture.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ss4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Question_Id",
                table: "answer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Question_Id",
                table: "answer");
        }
    }
}
