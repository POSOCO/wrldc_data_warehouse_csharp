using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class fsc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fscs",
                columns: table => new
                {
                    FscId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    WebUatId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AcTransLineCktId = table.Column<int>(nullable: false),
                    SubstationId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    PercComp = table.Column<int>(nullable: false),
                    LineReactance = table.Column<int>(nullable: false),
                    CapacitiveReactance = table.Column<int>(nullable: false),
                    RatedMvarPhase = table.Column<int>(nullable: false),
                    RatedCurrentAmps = table.Column<int>(nullable: false),
                    CommDate = table.Column<DateTime>(nullable: false),
                    CodDate = table.Column<DateTime>(nullable: false),
                    DeCommDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fscs", x => x.FscId);
                    table.ForeignKey(
                        name: "FK_Fscs_AcTransLineCkts_AcTransLineCktId",
                        column: x => x.AcTransLineCktId,
                        principalTable: "AcTransLineCkts",
                        principalColumn: "AcTransLineCktId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fscs_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fscs_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "SubstationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FscOwners",
                columns: table => new
                {
                    FscId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FscOwners", x => new { x.FscId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_FscOwners_Fscs_FscId",
                        column: x => x.FscId,
                        principalTable: "Fscs",
                        principalColumn: "FscId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FscOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FscOwners_OwnerId",
                table: "FscOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Fscs_AcTransLineCktId",
                table: "Fscs",
                column: "AcTransLineCktId");

            migrationBuilder.CreateIndex(
                name: "IX_Fscs_Name",
                table: "Fscs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fscs_StateId",
                table: "Fscs",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Fscs_WebUatId",
                table: "Fscs",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fscs_SubstationId_AcTransLineCktId",
                table: "Fscs",
                columns: new[] { "SubstationId", "AcTransLineCktId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FscOwners");

            migrationBuilder.DropTable(
                name: "Fscs");
        }
    }
}
