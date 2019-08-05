using Microsoft.EntityFrameworkCore.Migrations;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class bay_unique_modify_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bays_SubstationId_BayTypeId_SourceEntityType_SourceEntityId~",
                table: "Bays");

            migrationBuilder.CreateIndex(
                name: "IX_Bays_SubstationId",
                table: "Bays",
                column: "SubstationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bays_SubstationId",
                table: "Bays");

            migrationBuilder.CreateIndex(
                name: "IX_Bays_SubstationId_BayTypeId_SourceEntityType_SourceEntityId~",
                table: "Bays",
                columns: new[] { "SubstationId", "BayTypeId", "SourceEntityType", "SourceEntityId", "DestEntityId", "DestEntityType" },
                unique: true);
        }
    }
}
