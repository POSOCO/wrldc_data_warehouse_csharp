using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class gen_stages_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneratorStages",
                columns: table => new
                {
                    GeneratorStageId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    GeneratingStationId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratorStages", x => x.GeneratorStageId);
                    table.ForeignKey(
                        name: "FK_GeneratorStages_GeneratingStations_GeneratingStationId",
                        column: x => x.GeneratingStationId,
                        principalTable: "GeneratingStations",
                        principalColumn: "GeneratingStationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorStages_GeneratingStationId",
                table: "GeneratorStages",
                column: "GeneratingStationId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorStages_WebUatId",
                table: "GeneratorStages",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorStages_Name_GeneratingStationId",
                table: "GeneratorStages",
                columns: new[] { "Name", "GeneratingStationId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratorStages");
        }
    }
}
