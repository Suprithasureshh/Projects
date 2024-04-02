using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimesheetPoject.Migrations
{
    /// <inheritdoc />
    public partial class abcde : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "month",
                table: "TS_table");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "month",
                table: "TS_table",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
