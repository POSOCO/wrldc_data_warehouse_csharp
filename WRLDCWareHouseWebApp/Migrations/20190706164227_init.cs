using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MartDailyFrequencySummaries",
                columns: table => new
                {
                    MartDailyFrequencySummaryId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DataDate = table.Column<DateTime>(type: "date", nullable: false),
                    AverageFrequency = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    StandardDeviationFrequency = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    FVIFrequency = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    NumOutOfIEGCHrs = table.Column<decimal>(type: "decimal(5,3)", nullable: false),
                    PercLess48_8 = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    PercLess49 = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    PercLess49_2 = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    PercLess49_5 = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    PercLess49_7 = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    PercLess49_9 = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    PercGreatEq49_9LessEq50_05 = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    PercGreat50_05 = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    MaxFrequency = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    MaxFrequencyTime = table.Column<DateTime>(nullable: false),
                    MinFrequency = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    MinFrequencyTime = table.Column<DateTime>(nullable: false),
                    MaxBlkFreq = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    MinBlkFreq = table.Column<decimal>(type: "decimal(4,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MartDailyFrequencySummaries", x => x.MartDailyFrequencySummaryId);
                });

            migrationBuilder.CreateTable(
                name: "RawFrequencies",
                columns: table => new
                {
                    RawFrequencyId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DataTime = table.Column<DateTime>(nullable: false),
                    Frequency = table.Column<decimal>(type: "decimal(5,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawFrequencies", x => x.RawFrequencyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MartDailyFrequencySummaries_DataDate",
                table: "MartDailyFrequencySummaries",
                column: "DataDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RawFrequencies_DataTime",
                table: "RawFrequencies",
                column: "DataTime",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MartDailyFrequencySummaries");

            migrationBuilder.DropTable(
                name: "RawFrequencies");
        }
    }
}
