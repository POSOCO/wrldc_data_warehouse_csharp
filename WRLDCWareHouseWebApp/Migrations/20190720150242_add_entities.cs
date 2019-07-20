using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class add_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConductorTypes",
                columns: table => new
                {
                    ConductorTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConductorTypes", x => x.ConductorTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    OwnerId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.OwnerId);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FullName = table.Column<string>(nullable: false),
                    ShortName = table.Column<string>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "VoltLevels",
                columns: table => new
                {
                    VoltLevelId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    EntityType = table.Column<string>(nullable: true),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoltLevels", x => x.VoltLevelId);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RegionId = table.Column<int>(nullable: false),
                    ShortName = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_States_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MajorSubstations",
                columns: table => new
                {
                    MajorSubstationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MajorSubstations", x => x.MajorSubstationId);
                    table.ForeignKey(
                        name: "FK_MajorSubstations_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Substations",
                columns: table => new
                {
                    SubstationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    VoltLevelId = table.Column<int>(nullable: false),
                    MajorSubstationId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    Classification = table.Column<string>(nullable: true),
                    BusbarScheme = table.Column<string>(nullable: false, defaultValue: "NA"),
                    CommDate = table.Column<DateTime>(nullable: false),
                    CodDate = table.Column<DateTime>(nullable: false),
                    DecommDate = table.Column<DateTime>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substations", x => x.SubstationId);
                    table.ForeignKey(
                        name: "FK_Substations_MajorSubstations_MajorSubstationId",
                        column: x => x.MajorSubstationId,
                        principalTable: "MajorSubstations",
                        principalColumn: "MajorSubstationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Substations_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Substations_VoltLevels_VoltLevelId",
                        column: x => x.VoltLevelId,
                        principalTable: "VoltLevels",
                        principalColumn: "VoltLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcTransmissionLines",
                columns: table => new
                {
                    AcTransmissionLineId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<int>(nullable: false),
                    FromSubstationId = table.Column<int>(nullable: false),
                    ToSubstationId = table.Column<int>(nullable: false),
                    VoltLevelId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcTransmissionLines", x => x.AcTransmissionLineId);
                    table.ForeignKey(
                        name: "FK_AcTransmissionLines_Substations_FromSubstationId",
                        column: x => x.FromSubstationId,
                        principalTable: "Substations",
                        principalColumn: "SubstationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcTransmissionLines_Substations_ToSubstationId",
                        column: x => x.ToSubstationId,
                        principalTable: "Substations",
                        principalColumn: "SubstationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcTransmissionLines_VoltLevels_VoltLevelId",
                        column: x => x.VoltLevelId,
                        principalTable: "VoltLevels",
                        principalColumn: "VoltLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    BusId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<int>(nullable: false),
                    BusNumber = table.Column<string>(nullable: true),
                    VoltLevelId = table.Column<int>(nullable: false),
                    SubstationId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.BusId);
                    table.ForeignKey(
                        name: "FK_Buses_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "SubstationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Buses_VoltLevels_VoltLevelId",
                        column: x => x.VoltLevelId,
                        principalTable: "VoltLevels",
                        principalColumn: "VoltLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubstationOwners",
                columns: table => new
                {
                    SubstationId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubstationOwners", x => new { x.SubstationId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_SubstationOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubstationOwners_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "SubstationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcTransLineCkts",
                columns: table => new
                {
                    AcTransLineCktId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    CktNumber = table.Column<string>(nullable: true),
                    AcTransmissionLineId = table.Column<int>(nullable: false),
                    ConductorTypeId = table.Column<int>(nullable: false),
                    VoltLevelId = table.Column<int>(nullable: false),
                    Length = table.Column<decimal>(nullable: false),
                    ThermalLimitMVA = table.Column<decimal>(nullable: false),
                    SIL = table.Column<decimal>(nullable: false),
                    FtcDate = table.Column<DateTime>(nullable: false),
                    TrialOperationDate = table.Column<DateTime>(nullable: false),
                    CommDate = table.Column<DateTime>(nullable: false),
                    CODDate = table.Column<DateTime>(nullable: false),
                    DeCommDate = table.Column<DateTime>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcTransLineCkts", x => x.AcTransLineCktId);
                    table.ForeignKey(
                        name: "FK_AcTransLineCkts_AcTransmissionLines_AcTransmissionLineId",
                        column: x => x.AcTransmissionLineId,
                        principalTable: "AcTransmissionLines",
                        principalColumn: "AcTransmissionLineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcTransLineCkts_ConductorTypes_ConductorTypeId",
                        column: x => x.ConductorTypeId,
                        principalTable: "ConductorTypes",
                        principalColumn: "ConductorTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcTransLineCkts_VoltLevels_VoltLevelId",
                        column: x => x.VoltLevelId,
                        principalTable: "VoltLevels",
                        principalColumn: "VoltLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcTransLineCktOwners",
                columns: table => new
                {
                    AcTransLineCktId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcTransLineCktOwners", x => new { x.AcTransLineCktId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_AcTransLineCktOwners_AcTransLineCkts_AcTransLineCktId",
                        column: x => x.AcTransLineCktId,
                        principalTable: "AcTransLineCkts",
                        principalColumn: "AcTransLineCktId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcTransLineCktOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcTransLineCktOwners_OwnerId",
                table: "AcTransLineCktOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AcTransLineCkts_AcTransmissionLineId",
                table: "AcTransLineCkts",
                column: "AcTransmissionLineId");

            migrationBuilder.CreateIndex(
                name: "IX_AcTransLineCkts_ConductorTypeId",
                table: "AcTransLineCkts",
                column: "ConductorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AcTransLineCkts_Name",
                table: "AcTransLineCkts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcTransLineCkts_VoltLevelId",
                table: "AcTransLineCkts",
                column: "VoltLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AcTransLineCkts_WebUatId",
                table: "AcTransLineCkts",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcTransLineCkts_AcTransLineCktId_CktNumber",
                table: "AcTransLineCkts",
                columns: new[] { "AcTransLineCktId", "CktNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcTransmissionLines_FromSubstationId",
                table: "AcTransmissionLines",
                column: "FromSubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_AcTransmissionLines_Name",
                table: "AcTransmissionLines",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcTransmissionLines_ToSubstationId",
                table: "AcTransmissionLines",
                column: "ToSubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_AcTransmissionLines_VoltLevelId",
                table: "AcTransmissionLines",
                column: "VoltLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AcTransmissionLines_WebUatId",
                table: "AcTransmissionLines",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buses_Name",
                table: "Buses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buses_VoltLevelId",
                table: "Buses",
                column: "VoltLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_WebUatId",
                table: "Buses",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buses_SubstationId_BusNumber",
                table: "Buses",
                columns: new[] { "SubstationId", "BusNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConductorTypes_Name",
                table: "ConductorTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConductorTypes_WebUatId",
                table: "ConductorTypes",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MajorSubstations_Name",
                table: "MajorSubstations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MajorSubstations_StateId",
                table: "MajorSubstations",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_MajorSubstations_WebUatId",
                table: "MajorSubstations",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_Name",
                table: "Owners",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_WebUatId",
                table: "Owners",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_FullName",
                table: "Regions",
                column: "FullName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_ShortName",
                table: "Regions",
                column: "ShortName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_WebUatId",
                table: "Regions",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_FullName",
                table: "States",
                column: "FullName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_RegionId",
                table: "States",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_States_ShortName",
                table: "States",
                column: "ShortName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_WebUatId",
                table: "States",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubstationOwners_OwnerId",
                table: "SubstationOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Substations_MajorSubstationId",
                table: "Substations",
                column: "MajorSubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_Substations_Name",
                table: "Substations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Substations_StateId",
                table: "Substations",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Substations_VoltLevelId",
                table: "Substations",
                column: "VoltLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Substations_WebUatId",
                table: "Substations",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoltLevels_Name",
                table: "VoltLevels",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoltLevels_WebUatId",
                table: "VoltLevels",
                column: "WebUatId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcTransLineCktOwners");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "SubstationOwners");

            migrationBuilder.DropTable(
                name: "AcTransLineCkts");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "AcTransmissionLines");

            migrationBuilder.DropTable(
                name: "ConductorTypes");

            migrationBuilder.DropTable(
                name: "Substations");

            migrationBuilder.DropTable(
                name: "MajorSubstations");

            migrationBuilder.DropTable(
                name: "VoltLevels");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
