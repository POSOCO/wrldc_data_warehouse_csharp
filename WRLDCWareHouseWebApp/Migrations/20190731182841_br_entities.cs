using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class br_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusReactors",
                columns: table => new
                {
                    BusReactorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    BusReactorNumber = table.Column<int>(nullable: false),
                    MvarCapacity = table.Column<decimal>(nullable: false),
                    CommDate = table.Column<DateTime>(nullable: false),
                    CodDate = table.Column<DateTime>(nullable: false),
                    DecommDate = table.Column<DateTime>(nullable: false),
                    BusId = table.Column<int>(nullable: false),
                    SubstationId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusReactors", x => x.BusReactorId);
                    table.ForeignKey(
                        name: "FK_BusReactors_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusReactors_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusReactors_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "SubstationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusReactorOwners",
                columns: table => new
                {
                    BusReactorId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusReactorOwners", x => new { x.BusReactorId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_BusReactorOwners_BusReactors_BusReactorId",
                        column: x => x.BusReactorId,
                        principalTable: "BusReactors",
                        principalColumn: "BusReactorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusReactorOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusReactorOwners_OwnerId",
                table: "BusReactorOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BusReactors_BusId",
                table: "BusReactors",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_BusReactors_Name",
                table: "BusReactors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusReactors_StateId",
                table: "BusReactors",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_BusReactors_SubstationId",
                table: "BusReactors",
                column: "SubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_BusReactors_WebUatId",
                table: "BusReactors",
                column: "WebUatId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusReactorOwners");

            migrationBuilder.DropTable(
                name: "BusReactors");
        }
    }
}
