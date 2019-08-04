using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class hvdcpole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HvdcPoles",
                columns: table => new
                {
                    HvdcPoleId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    PoleNumber = table.Column<string>(nullable: true),
                    SubstationId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    VoltLevelId = table.Column<int>(nullable: false),
                    PoleType = table.Column<string>(nullable: true),
                    MaxFiringAngleDegrees = table.Column<decimal>(nullable: false),
                    MinFiringAngleDegrees = table.Column<decimal>(nullable: false),
                    ThermalLimitMVA = table.Column<decimal>(nullable: false),
                    CommDate = table.Column<DateTime>(nullable: false),
                    CodDate = table.Column<DateTime>(nullable: false),
                    DeCommDate = table.Column<DateTime>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HvdcPoles", x => x.HvdcPoleId);
                    table.ForeignKey(
                        name: "FK_HvdcPoles_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HvdcPoles_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "SubstationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HvdcPoles_VoltLevels_VoltLevelId",
                        column: x => x.VoltLevelId,
                        principalTable: "VoltLevels",
                        principalColumn: "VoltLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HvdcPoleOwners",
                columns: table => new
                {
                    HvdcPoleId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HvdcPoleOwners", x => new { x.HvdcPoleId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_HvdcPoleOwners_HvdcPoles_HvdcPoleId",
                        column: x => x.HvdcPoleId,
                        principalTable: "HvdcPoles",
                        principalColumn: "HvdcPoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HvdcPoleOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HvdcPoleOwners_OwnerId",
                table: "HvdcPoleOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_HvdcPoles_Name",
                table: "HvdcPoles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HvdcPoles_StateId",
                table: "HvdcPoles",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_HvdcPoles_VoltLevelId",
                table: "HvdcPoles",
                column: "VoltLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_HvdcPoles_WebUatId",
                table: "HvdcPoles",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HvdcPoles_SubstationId_PoleNumber",
                table: "HvdcPoles",
                columns: new[] { "SubstationId", "PoleNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HvdcPoleOwners");

            migrationBuilder.DropTable(
                name: "HvdcPoles");
        }
    }
}
