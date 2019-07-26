using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class trans_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransformerTypes",
                columns: table => new
                {
                    TransformerTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransformerTypes", x => x.TransformerTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Transformers",
                columns: table => new
                {
                    TransformerId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    StationType = table.Column<string>(nullable: false),
                    HighVoltLevelId = table.Column<int>(nullable: false),
                    LowVoltLevelId = table.Column<int>(nullable: false),
                    HvSubstationId = table.Column<int>(nullable: true),
                    HvGeneratingStationId = table.Column<int>(nullable: true),
                    TransformerNumber = table.Column<int>(nullable: false),
                    TransformerTypeId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    MVACapacity = table.Column<decimal>(nullable: false),
                    CommDate = table.Column<DateTime>(nullable: false),
                    CodDate = table.Column<DateTime>(nullable: false),
                    DecommDate = table.Column<DateTime>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transformers", x => x.TransformerId);
                    table.ForeignKey(
                        name: "FK_Transformers_VoltLevels_HighVoltLevelId",
                        column: x => x.HighVoltLevelId,
                        principalTable: "VoltLevels",
                        principalColumn: "VoltLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transformers_GeneratingStations_HvGeneratingStationId",
                        column: x => x.HvGeneratingStationId,
                        principalTable: "GeneratingStations",
                        principalColumn: "GeneratingStationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transformers_MajorSubstations_HvSubstationId",
                        column: x => x.HvSubstationId,
                        principalTable: "MajorSubstations",
                        principalColumn: "MajorSubstationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transformers_VoltLevels_LowVoltLevelId",
                        column: x => x.LowVoltLevelId,
                        principalTable: "VoltLevels",
                        principalColumn: "VoltLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transformers_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transformers_TransformerTypes_TransformerTypeId",
                        column: x => x.TransformerTypeId,
                        principalTable: "TransformerTypes",
                        principalColumn: "TransformerTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransformerOwners",
                columns: table => new
                {
                    TransformerId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransformerOwners", x => new { x.TransformerId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_TransformerOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransformerOwners_Transformers_TransformerId",
                        column: x => x.TransformerId,
                        principalTable: "Transformers",
                        principalColumn: "TransformerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransformerOwners_OwnerId",
                table: "TransformerOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transformers_HighVoltLevelId",
                table: "Transformers",
                column: "HighVoltLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Transformers_HvGeneratingStationId",
                table: "Transformers",
                column: "HvGeneratingStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transformers_HvSubstationId",
                table: "Transformers",
                column: "HvSubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transformers_LowVoltLevelId",
                table: "Transformers",
                column: "LowVoltLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Transformers_Name",
                table: "Transformers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transformers_StateId",
                table: "Transformers",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Transformers_TransformerTypeId",
                table: "Transformers",
                column: "TransformerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transformers_WebUatId",
                table: "Transformers",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransformerTypes_Name",
                table: "TransformerTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransformerTypes_WebUatId",
                table: "TransformerTypes",
                column: "WebUatId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransformerOwners");

            migrationBuilder.DropTable(
                name: "Transformers");

            migrationBuilder.DropTable(
                name: "TransformerTypes");
        }
    }
}
