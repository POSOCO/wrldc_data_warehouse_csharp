using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class hvdclines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HvdcLines",
                columns: table => new
                {
                    HvdcLineId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    FromSubstationId = table.Column<int>(nullable: false),
                    ToSubstationId = table.Column<int>(nullable: false),
                    VoltLevelId = table.Column<int>(nullable: false),
                    FromStateId = table.Column<int>(nullable: false),
                    ToStateId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HvdcLines", x => x.HvdcLineId);
                    table.ForeignKey(
                        name: "FK_HvdcLines_States_FromStateId",
                        column: x => x.FromStateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HvdcLines_Substations_FromSubstationId",
                        column: x => x.FromSubstationId,
                        principalTable: "Substations",
                        principalColumn: "SubstationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HvdcLines_States_ToStateId",
                        column: x => x.ToStateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HvdcLines_Substations_ToSubstationId",
                        column: x => x.ToSubstationId,
                        principalTable: "Substations",
                        principalColumn: "SubstationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HvdcLines_VoltLevels_VoltLevelId",
                        column: x => x.VoltLevelId,
                        principalTable: "VoltLevels",
                        principalColumn: "VoltLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HvdcLineCkts",
                columns: table => new
                {
                    HvdcLineCktId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    CktNumber = table.Column<string>(nullable: true),
                    HvdcLineId = table.Column<int>(nullable: false),
                    FromBusId = table.Column<int>(nullable: false),
                    ToBusId = table.Column<int>(nullable: false),
                    NumConductorsPerCkt = table.Column<int>(nullable: false),
                    Length = table.Column<decimal>(nullable: false),
                    ThermalLimitMVA = table.Column<decimal>(nullable: false),
                    FtcDate = table.Column<DateTime>(nullable: false),
                    TrialOperationDate = table.Column<DateTime>(nullable: false),
                    CommDate = table.Column<DateTime>(nullable: false),
                    CodDate = table.Column<DateTime>(nullable: false),
                    DeCommDate = table.Column<DateTime>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HvdcLineCkts", x => x.HvdcLineCktId);
                    table.ForeignKey(
                        name: "FK_HvdcLineCkts_Buses_FromBusId",
                        column: x => x.FromBusId,
                        principalTable: "Buses",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HvdcLineCkts_HvdcLines_HvdcLineId",
                        column: x => x.HvdcLineId,
                        principalTable: "HvdcLines",
                        principalColumn: "HvdcLineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HvdcLineCkts_Buses_ToBusId",
                        column: x => x.ToBusId,
                        principalTable: "Buses",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HvdcLineCktOwners",
                columns: table => new
                {
                    HvdcLineCktId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HvdcLineCktOwners", x => new { x.HvdcLineCktId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_HvdcLineCktOwners_HvdcLineCkts_HvdcLineCktId",
                        column: x => x.HvdcLineCktId,
                        principalTable: "HvdcLineCkts",
                        principalColumn: "HvdcLineCktId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HvdcLineCktOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLineCktOwners_OwnerId",
                table: "HvdcLineCktOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLineCkts_FromBusId",
                table: "HvdcLineCkts",
                column: "FromBusId");

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLineCkts_HvdcLineId",
                table: "HvdcLineCkts",
                column: "HvdcLineId");

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLineCkts_Name",
                table: "HvdcLineCkts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLineCkts_ToBusId",
                table: "HvdcLineCkts",
                column: "ToBusId");

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLineCkts_WebUatId",
                table: "HvdcLineCkts",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLineCkts_HvdcLineCktId_CktNumber",
                table: "HvdcLineCkts",
                columns: new[] { "HvdcLineCktId", "CktNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLines_FromStateId",
                table: "HvdcLines",
                column: "FromStateId");

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLines_Name",
                table: "HvdcLines",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLines_ToStateId",
                table: "HvdcLines",
                column: "ToStateId");

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLines_ToSubstationId",
                table: "HvdcLines",
                column: "ToSubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLines_VoltLevelId",
                table: "HvdcLines",
                column: "VoltLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLines_WebUatId",
                table: "HvdcLines",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HvdcLines_FromSubstationId_ToSubstationId",
                table: "HvdcLines",
                columns: new[] { "FromSubstationId", "ToSubstationId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HvdcLineCktOwners");

            migrationBuilder.DropTable(
                name: "HvdcLineCkts");

            migrationBuilder.DropTable(
                name: "HvdcLines");
        }
    }
}
