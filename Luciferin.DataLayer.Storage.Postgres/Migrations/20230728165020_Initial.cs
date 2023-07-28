using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luciferin.DataLayer.Storage.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportStatistics",
                columns: table => new
                {
                    ImportDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExistingTransactionsFiltered = table.Column<int>(type: "integer", nullable: false),
                    NewTransactions = table.Column<int>(type: "integer", nullable: false),
                    StartingBalanceSet = table.Column<bool>(type: "boolean", nullable: false),
                    TotalAccounts = table.Column<int>(type: "integer", nullable: false),
                    TotalFireflyTransactions = table.Column<int>(type: "integer", nullable: false),
                    TotalRetrievedTransactions = table.Column<int>(type: "integer", nullable: false),
                    TransfersFiltered = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportStatistics", x => x.ImportDate);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Key = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ValueType = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    BooleanValue = table.Column<bool>(type: "boolean", nullable: true),
                    IntValue = table.Column<int>(type: "integer", nullable: true),
                    StringValue = table.Column<string>(type: "text", nullable: false),
                    TimeSpanValue = table.Column<TimeSpan>(type: "interval", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Key);
                });
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
