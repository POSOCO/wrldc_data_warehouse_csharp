using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class gen_units_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GeneratorStages",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GeneratorUnits",
                columns: table => new
                {
                    GeneratorUnitId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    UnitNumber = table.Column<string>(nullable: true),
                    GeneratingStationId = table.Column<int>(nullable: false),
                    GeneratorStageId = table.Column<int>(nullable: false),
                    GenVoltageKV = table.Column<decimal>(nullable: false),
                    GenHighVoltageKV = table.Column<decimal>(nullable: false),
                    MvaCapacity = table.Column<decimal>(nullable: false),
                    InstalledCapacity = table.Column<decimal>(nullable: false),
                    CommDateTime = table.Column<DateTime>(nullable: false),
                    CodDateTime = table.Column<DateTime>(nullable: false),
                    DeCommDateTime = table.Column<DateTime>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratorUnits", x => x.GeneratorUnitId);
                    table.ForeignKey(
                        name: "FK_GeneratorUnits_GeneratingStations_GeneratingStationId",
                        column: x => x.GeneratingStationId,
                        principalTable: "GeneratingStations",
                        principalColumn: "GeneratingStationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneratorUnits_GeneratorStages_GeneratorStageId",
                        column: x => x.GeneratorStageId,
                        principalTable: "GeneratorStages",
                        principalColumn: "GeneratorStageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorUnits_GeneratingStationId",
                table: "GeneratorUnits",
                column: "GeneratingStationId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorUnits_GeneratorStageId",
                table: "GeneratorUnits",
                column: "GeneratorStageId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorUnits_WebUatId",
                table: "GeneratorUnits",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorUnits_UnitNumber_GeneratingStationId_GeneratorStag~",
                table: "GeneratorUnits",
                columns: new[] { "UnitNumber", "GeneratingStationId", "GeneratorStageId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratorUnits");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GeneratorStages",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
