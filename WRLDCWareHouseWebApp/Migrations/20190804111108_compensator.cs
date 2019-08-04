using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class compensator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompensatorTypes",
                columns: table => new
                {
                    CompensatorTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompensatorTypes", x => x.CompensatorTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Compensators",
                columns: table => new
                {
                    CompensatorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    WebUatId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CompensatorTypeId = table.Column<int>(nullable: false),
                    SubstationId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    AttachElementType = table.Column<int>(nullable: false),
                    AttachElementId = table.Column<int>(nullable: false),
                    CompensatorNumber = table.Column<string>(nullable: true),
                    PercVariableComp = table.Column<int>(nullable: false),
                    PercFixedComp = table.Column<int>(nullable: false),
                    LineReactanceOhms = table.Column<int>(nullable: false),
                    CommDate = table.Column<DateTime>(nullable: false),
                    CodDate = table.Column<DateTime>(nullable: false),
                    DeCommDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compensators", x => x.CompensatorId);
                    table.ForeignKey(
                        name: "FK_Compensators_CompensatorTypes_CompensatorTypeId",
                        column: x => x.CompensatorTypeId,
                        principalTable: "CompensatorTypes",
                        principalColumn: "CompensatorTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compensators_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compensators_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "SubstationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompensatorOwners",
                columns: table => new
                {
                    CompensatorId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompensatorOwners", x => new { x.CompensatorId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_CompensatorOwners_Compensators_CompensatorId",
                        column: x => x.CompensatorId,
                        principalTable: "Compensators",
                        principalColumn: "CompensatorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompensatorOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompensatorOwners_OwnerId",
                table: "CompensatorOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Compensators_CompensatorTypeId",
                table: "Compensators",
                column: "CompensatorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Compensators_Name",
                table: "Compensators",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compensators_StateId",
                table: "Compensators",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Compensators_WebUatId",
                table: "Compensators",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compensators_SubstationId_AttachElementType_AttachElementId~",
                table: "Compensators",
                columns: new[] { "SubstationId", "AttachElementType", "AttachElementId", "CompensatorNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompensatorTypes_Name",
                table: "CompensatorTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompensatorTypes_WebUatId",
                table: "CompensatorTypes",
                column: "WebUatId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompensatorOwners");

            migrationBuilder.DropTable(
                name: "Compensators");

            migrationBuilder.DropTable(
                name: "CompensatorTypes");
        }
    }
}
