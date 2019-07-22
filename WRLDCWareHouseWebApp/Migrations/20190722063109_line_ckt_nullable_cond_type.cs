using Microsoft.EntityFrameworkCore.Migrations;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class line_ckt_nullable_cond_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcTransLineCkts_ConductorTypes_ConductorTypeId",
                table: "AcTransLineCkts");

            migrationBuilder.AlterColumn<int>(
                name: "ConductorTypeId",
                table: "AcTransLineCkts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AcTransLineCkts_ConductorTypes_ConductorTypeId",
                table: "AcTransLineCkts",
                column: "ConductorTypeId",
                principalTable: "ConductorTypes",
                principalColumn: "ConductorTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcTransLineCkts_ConductorTypes_ConductorTypeId",
                table: "AcTransLineCkts");

            migrationBuilder.AlterColumn<int>(
                name: "ConductorTypeId",
                table: "AcTransLineCkts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AcTransLineCkts_ConductorTypes_ConductorTypeId",
                table: "AcTransLineCkts",
                column: "ConductorTypeId",
                principalTable: "ConductorTypes",
                principalColumn: "ConductorTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
