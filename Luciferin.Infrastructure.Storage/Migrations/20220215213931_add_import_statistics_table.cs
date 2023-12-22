using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luciferin.Infrastructure.Storage.Migrations
{
    public partial class add_import_statistics_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportStatistics");
        }
    }
}
