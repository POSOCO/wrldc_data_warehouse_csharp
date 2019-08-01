using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class lr_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LineReactors",
                columns: table => new
                {
                    LineReactorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    MvarCapacity = table.Column<decimal>(nullable: false),
                    CommDate = table.Column<DateTime>(nullable: false),
                    CodDate = table.Column<DateTime>(nullable: false),
                    DecommDate = table.Column<DateTime>(nullable: false),
                    IsConvertible = table.Column<bool>(nullable: false),
                    IsSwitchable = table.Column<bool>(nullable: false),
                    AcTransLineCktId = table.Column<int>(nullable: false),
                    SubstationId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineReactors", x => x.LineReactorId);
                    table.ForeignKey(
                        name: "FK_LineReactors_AcTransLineCkts_AcTransLineCktId",
                        column: x => x.AcTransLineCktId,
                        principalTable: "AcTransLineCkts",
                        principalColumn: "AcTransLineCktId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineReactors_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineReactors_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "SubstationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineReactorOwners",
                columns: table => new
                {
                    LineReactorId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineReactorOwners", x => new { x.LineReactorId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_LineReactorOwners_LineReactors_LineReactorId",
                        column: x => x.LineReactorId,
                        principalTable: "LineReactors",
                        principalColumn: "LineReactorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineReactorOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineReactorOwners_OwnerId",
                table: "LineReactorOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LineReactors_AcTransLineCktId",
                table: "LineReactors",
                column: "AcTransLineCktId");

            migrationBuilder.CreateIndex(
                name: "IX_LineReactors_Name",
                table: "LineReactors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LineReactors_StateId",
                table: "LineReactors",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_LineReactors_SubstationId",
                table: "LineReactors",
                column: "SubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_LineReactors_WebUatId",
                table: "LineReactors",
                column: "WebUatId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineReactorOwners");

            migrationBuilder.DropTable(
                name: "LineReactors");
        }
    }
}
