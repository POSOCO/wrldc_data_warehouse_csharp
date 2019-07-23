using Microsoft.EntityFrameworkCore.Migrations;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class gen_station_name_unique_modify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GeneratingStations_Name_GeneratorClassificationId",
                table: "GeneratingStations");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStations_Name_GeneratorClassificationId_StateId",
                table: "GeneratingStations",
                columns: new[] { "Name", "GeneratorClassificationId", "StateId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GeneratingStations_Name_GeneratorClassificationId_StateId",
                table: "GeneratingStations");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratingStations_Name_GeneratorClassificationId",
                table: "GeneratingStations",
                columns: new[] { "Name", "GeneratorClassificationId" },
                unique: true);
        }
    }
}
