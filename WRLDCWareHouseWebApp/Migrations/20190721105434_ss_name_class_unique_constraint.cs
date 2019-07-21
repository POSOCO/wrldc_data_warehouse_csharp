using Microsoft.EntityFrameworkCore.Migrations;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class ss_name_class_unique_constraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Substations_Name",
                table: "Substations");

            migrationBuilder.CreateIndex(
                name: "IX_Substations_Name_Classification",
                table: "Substations",
                columns: new[] { "Name", "Classification" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Substations_Name_Classification",
                table: "Substations");

            migrationBuilder.CreateIndex(
                name: "IX_Substations_Name",
                table: "Substations",
                column: "Name",
                unique: true);
        }
    }
}
