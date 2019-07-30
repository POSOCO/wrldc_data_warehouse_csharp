using Microsoft.EntityFrameworkCore.Migrations;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class nullable_majorssid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Substations_MajorSubstations_MajorSubstationId",
                table: "Substations");

            migrationBuilder.AlterColumn<int>(
                name: "MajorSubstationId",
                table: "Substations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Substations_MajorSubstations_MajorSubstationId",
                table: "Substations",
                column: "MajorSubstationId",
                principalTable: "MajorSubstations",
                principalColumn: "MajorSubstationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Substations_MajorSubstations_MajorSubstationId",
                table: "Substations");

            migrationBuilder.AlterColumn<int>(
                name: "MajorSubstationId",
                table: "Substations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Substations_MajorSubstations_MajorSubstationId",
                table: "Substations",
                column: "MajorSubstationId",
                principalTable: "MajorSubstations",
                principalColumn: "MajorSubstationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
