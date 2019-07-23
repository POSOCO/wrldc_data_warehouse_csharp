using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class fuel_gen_type_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fuels",
                columns: table => new
                {
                    FuelId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuels", x => x.FuelId);
                });

            migrationBuilder.CreateTable(
                name: "GenerationTypes",
                columns: table => new
                {
                    GenerationTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenerationTypes", x => x.GenerationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "GenerationTypeFuels",
                columns: table => new
                {
                    GenerationTypeId = table.Column<int>(nullable: false),
                    FuelId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenerationTypeFuels", x => new { x.GenerationTypeId, x.FuelId });
                    table.ForeignKey(
                        name: "FK_GenerationTypeFuels_Fuels_FuelId",
                        column: x => x.FuelId,
                        principalTable: "Fuels",
                        principalColumn: "FuelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenerationTypeFuels_GenerationTypes_GenerationTypeId",
                        column: x => x.GenerationTypeId,
                        principalTable: "GenerationTypes",
                        principalColumn: "GenerationTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fuels_Name",
                table: "Fuels",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fuels_WebUatId",
                table: "Fuels",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GenerationTypeFuels_FuelId",
                table: "GenerationTypeFuels",
                column: "FuelId");

            migrationBuilder.CreateIndex(
                name: "IX_GenerationTypes_Name",
                table: "GenerationTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GenerationTypes_WebUatId",
                table: "GenerationTypes",
                column: "WebUatId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenerationTypeFuels");

            migrationBuilder.DropTable(
                name: "Fuels");

            migrationBuilder.DropTable(
                name: "GenerationTypes");
        }
    }
}
