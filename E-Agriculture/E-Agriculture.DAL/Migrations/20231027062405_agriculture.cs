using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Agriculture.DAL.Migrations
{
    /// <inheritdoc />
    public partial class agriculture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "answer",
                columns: table => new
                {
                    Answer_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerFor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answer", x => x.Answer_Id);
                });

            migrationBuilder.CreateTable(
                name: "availabilityCrops",
                columns: table => new
                {
                    ACrop_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ACrop_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AQuantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ALocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Add_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_availabilityCrops", x => x.ACrop_Id);
                });

            migrationBuilder.CreateTable(
                name: "governmentPrograms",
                columns: table => new
                {
                    Program_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Program_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Program_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramStart_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProgramEnd_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_governmentPrograms", x => x.Program_Id);
                });

            migrationBuilder.CreateTable(
                name: "marketDetails",
                columns: table => new
                {
                    MCrop_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MCrop_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MQuantity = table.Column<int>(type: "int", nullable: false),
                    MPrice = table.Column<int>(type: "int", nullable: false),
                    MLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MAdd_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marketDetails", x => x.MCrop_Id);
                });

            migrationBuilder.CreateTable(
                name: "queries",
                columns: table => new
                {
                    Question_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_queries", x => x.Question_Id);
                });

            migrationBuilder.CreateTable(
                name: "requiredCrops",
                columns: table => new
                {
                    RCrop_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RCrop_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RQuantity = table.Column<int>(type: "int", nullable: false),
                    RLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RAdd_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requiredCrops", x => x.RCrop_Id);
                });

            migrationBuilder.CreateTable(
                name: "userdetails",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact_No = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Joining_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hashpassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userdetails", x => x.User_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answer");

            migrationBuilder.DropTable(
                name: "availabilityCrops");

            migrationBuilder.DropTable(
                name: "governmentPrograms");

            migrationBuilder.DropTable(
                name: "marketDetails");

            migrationBuilder.DropTable(
                name: "queries");

            migrationBuilder.DropTable(
                name: "requiredCrops");

            migrationBuilder.DropTable(
                name: "userdetails");
        }
    }
}
