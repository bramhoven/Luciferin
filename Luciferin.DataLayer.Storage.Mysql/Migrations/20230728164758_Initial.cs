using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luciferin.DataLayer.Storage.Mysql.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ImportStatistics",
                columns: table => new
                {
                    ImportDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExistingTransactionsFiltered = table.Column<int>(type: "int", nullable: false),
                    NewTransactions = table.Column<int>(type: "int", nullable: false),
                    StartingBalanceSet = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TotalAccounts = table.Column<int>(type: "int", nullable: false),
                    TotalFireflyTransactions = table.Column<int>(type: "int", nullable: false),
                    TotalRetrievedTransactions = table.Column<int>(type: "int", nullable: false),
                    TransfersFiltered = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportStatistics", x => x.ImportDate);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Key = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValueType = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BooleanValue = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    IntValue = table.Column<int>(type: "int", nullable: true),
                    StringValue = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeSpanValue = table.Column<TimeSpan>(type: "time(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Key);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportStatistics");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
