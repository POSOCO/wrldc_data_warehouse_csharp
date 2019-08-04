using Microsoft.EntityFrameworkCore.Migrations;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class compensator_unique_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Compensators_SubstationId_AttachElementType_AttachElementId~",
                table: "Compensators");

            migrationBuilder.CreateIndex(
                name: "IX_Compensators_SubstationId_AttachElementType_AttachElementId~",
                table: "Compensators",
                columns: new[] { "SubstationId", "AttachElementType", "AttachElementId", "CompensatorNumber", "CompensatorTypeId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Compensators_SubstationId_AttachElementType_AttachElementId~",
                table: "Compensators");

            migrationBuilder.CreateIndex(
                name: "IX_Compensators_SubstationId_AttachElementType_AttachElementId~",
                table: "Compensators",
                columns: new[] { "SubstationId", "AttachElementType", "AttachElementId", "CompensatorNumber" },
                unique: true);
        }
    }
}
