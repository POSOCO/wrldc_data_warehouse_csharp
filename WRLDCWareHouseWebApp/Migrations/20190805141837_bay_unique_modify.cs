using Microsoft.EntityFrameworkCore.Migrations;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class bay_unique_modify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bays_SubstationId",
                table: "Bays");

            migrationBuilder.DropIndex(
                name: "IX_Bays_BayTypeId_SourceEntityType_SourceEntityId_DestEntityId~",
                table: "Bays");

            migrationBuilder.CreateIndex(
                name: "IX_Bays_BayTypeId",
                table: "Bays",
                column: "BayTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bays_SubstationId_BayTypeId_SourceEntityType_SourceEntityId~",
                table: "Bays",
                columns: new[] { "SubstationId", "BayTypeId", "SourceEntityType", "SourceEntityId", "DestEntityId", "DestEntityType" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bays_BayTypeId",
                table: "Bays");

            migrationBuilder.DropIndex(
                name: "IX_Bays_SubstationId_BayTypeId_SourceEntityType_SourceEntityId~",
                table: "Bays");

            migrationBuilder.CreateIndex(
                name: "IX_Bays_SubstationId",
                table: "Bays",
                column: "SubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bays_BayTypeId_SourceEntityType_SourceEntityId_DestEntityId~",
                table: "Bays",
                columns: new[] { "BayTypeId", "SourceEntityType", "SourceEntityId", "DestEntityId", "DestEntityType" },
                unique: true);
        }
    }
}
