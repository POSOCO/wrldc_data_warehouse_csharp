using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class gen_entities_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneratorClassifications",
                columns: table => new
                {
                    GeneratorClassificationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratorClassifications", x => x.GeneratorClassificationId);
                });

            migrationBuilder.CreateTable(
                name: "GeneratingStations",
                columns: table => new
                {
                    GeneratingStationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    GeneratorClassificationId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    GenerationTypeId = table.Column<int>(nullable: false),
                    FuelId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratingStations", x => x.GeneratingStationId);
                    table.ForeignKey(
                        name: "FK_GeneratingStations_Fuels_FuelId",
                        column: x => x.FuelId,
                        principalTable: "Fuels",
                        principalColumn: "FuelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneratingStations_GenerationTypes_GenerationTypeId",
                        column: x => x.GenerationTypeId,
                        principalTable: "GenerationTypes",
                        principalColumn: "GenerationTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneratingStations_GeneratorClassifications_GeneratorClassi~",
                        column: x => x.GeneratorClassificationId,
                        principalTable: "GeneratorClassifications",
                        principalColumn: "GeneratorClassificationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneratingStations_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneratingStationOwners",
                columns: table => new
                {
                    GeneratingStationId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratingStationOwners", x => new { x.GeneratingStationId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_GeneratingStationOwners_GeneratingStations_GeneratingStatio~",
                        column: x => x.GeneratingStationId,
                        principalTable: "GeneratingStations",
                        principalColumn: "GeneratingStationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneratingStationOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStationOwners_OwnerId",
                table: "GeneratingStationOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStations_FuelId",
                table: "GeneratingStations",
                column: "FuelId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStations_GenerationTypeId",
                table: "GeneratingStations",
                column: "GenerationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStations_GeneratorClassificationId",
                table: "GeneratingStations",
                column: "GeneratorClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStations_StateId",
                table: "GeneratingStations",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStations_WebUatId",
                table: "GeneratingStations",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStations_Name_GeneratorClassificationId",
                table: "GeneratingStations",
                columns: new[] { "Name", "GeneratorClassificationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorClassifications_Name",
                table: "GeneratorClassifications",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorClassifications_WebUatId",
                table: "GeneratorClassifications",
                column: "WebUatId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratingStationOwners");

            migrationBuilder.DropTable(
                name: "GeneratingStations");

            migrationBuilder.DropTable(
                name: "GeneratorClassifications");
        }
    }
}
