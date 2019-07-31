using Microsoft.EntityFrameworkCore.Migrations;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class null_trans_lv_volt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transformers_VoltLevels_LowVoltLevelId",
                table: "Transformers");

            migrationBuilder.AlterColumn<int>(
                name: "LowVoltLevelId",
                table: "Transformers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Transformers_VoltLevels_LowVoltLevelId",
                table: "Transformers",
                column: "LowVoltLevelId",
                principalTable: "VoltLevels",
                principalColumn: "VoltLevelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transformers_VoltLevels_LowVoltLevelId",
                table: "Transformers");

            migrationBuilder.AlterColumn<int>(
                name: "LowVoltLevelId",
                table: "Transformers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transformers_VoltLevels_LowVoltLevelId",
                table: "Transformers",
                column: "LowVoltLevelId",
                principalTable: "VoltLevels",
                principalColumn: "VoltLevelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
