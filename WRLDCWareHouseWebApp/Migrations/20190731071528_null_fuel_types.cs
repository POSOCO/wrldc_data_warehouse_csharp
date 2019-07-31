using Microsoft.EntityFrameworkCore.Migrations;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class null_fuel_types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneratingStations_Fuels_FuelId",
                table: "GeneratingStations");

            migrationBuilder.AlterColumn<int>(
                name: "FuelId",
                table: "GeneratingStations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_GeneratingStations_Fuels_FuelId",
                table: "GeneratingStations",
                column: "FuelId",
                principalTable: "Fuels",
                principalColumn: "FuelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneratingStations_Fuels_FuelId",
                table: "GeneratingStations");

            migrationBuilder.AlterColumn<int>(
                name: "FuelId",
                table: "GeneratingStations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneratingStations_Fuels_FuelId",
                table: "GeneratingStations",
                column: "FuelId",
                principalTable: "Fuels",
                principalColumn: "FuelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
