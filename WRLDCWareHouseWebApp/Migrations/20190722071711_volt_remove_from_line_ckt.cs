using Microsoft.EntityFrameworkCore.Migrations;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class volt_remove_from_line_ckt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcTransLineCkts_VoltLevels_VoltLevelId",
                table: "AcTransLineCkts");

            migrationBuilder.DropIndex(
                name: "IX_AcTransLineCkts_VoltLevelId",
                table: "AcTransLineCkts");

            migrationBuilder.DropColumn(
                name: "VoltLevelId",
                table: "AcTransLineCkts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoltLevelId",
                table: "AcTransLineCkts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AcTransLineCkts_VoltLevelId",
                table: "AcTransLineCkts",
                column: "VoltLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcTransLineCkts_VoltLevels_VoltLevelId",
                table: "AcTransLineCkts",
                column: "VoltLevelId",
                principalTable: "VoltLevels",
                principalColumn: "VoltLevelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
