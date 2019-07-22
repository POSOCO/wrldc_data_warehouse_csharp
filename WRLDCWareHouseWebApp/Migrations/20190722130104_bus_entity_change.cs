using Microsoft.EntityFrameworkCore.Migrations;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class bus_entity_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Buses_SubstationId_BusNumber",
                table: "Buses");

            migrationBuilder.AddColumn<string>(
                name: "BusType",
                table: "Buses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buses_SubstationId",
                table: "Buses",
                column: "SubstationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Buses_SubstationId",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "BusType",
                table: "Buses");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_SubstationId_BusNumber",
                table: "Buses",
                columns: new[] { "SubstationId", "BusNumber" },
                unique: true);
        }
    }
}
