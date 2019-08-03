using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WRLDCWareHouseWebApp.Migrations
{
    public partial class filterbaks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BayTypes",
                columns: table => new
                {
                    BayTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    WebUatId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BayTypes", x => x.BayTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FilterBanks",
                columns: table => new
                {
                    FilterBankId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    SubstationId = table.Column<int>(nullable: false),
                    VoltLevelId = table.Column<int>(nullable: false),
                    IsSwitchable = table.Column<bool>(nullable: false),
                    FilterBankNumber = table.Column<string>(nullable: true),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterBanks", x => x.FilterBankId);
                    table.ForeignKey(
                        name: "FK_FilterBanks_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterBanks_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterBanks_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "SubstationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterBanks_VoltLevels_VoltLevelId",
                        column: x => x.VoltLevelId,
                        principalTable: "VoltLevels",
                        principalColumn: "VoltLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bays",
                columns: table => new
                {
                    BayId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    BayNumber = table.Column<string>(nullable: true),
                    SourceEntityId = table.Column<int>(nullable: false),
                    SourceEntityType = table.Column<string>(nullable: false),
                    SourceEntityName = table.Column<string>(nullable: true),
                    DestEntityId = table.Column<int>(nullable: false),
                    DestEntityType = table.Column<string>(nullable: true),
                    DestEntityName = table.Column<string>(nullable: true),
                    BayTypeId = table.Column<int>(nullable: false),
                    VoltLevelId = table.Column<int>(nullable: false),
                    SubstationId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bays", x => x.BayId);
                    table.ForeignKey(
                        name: "FK_Bays_BayTypes_BayTypeId",
                        column: x => x.BayTypeId,
                        principalTable: "BayTypes",
                        principalColumn: "BayTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bays_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "SubstationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bays_VoltLevels_VoltLevelId",
                        column: x => x.VoltLevelId,
                        principalTable: "VoltLevels",
                        principalColumn: "VoltLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterBankOwners",
                columns: table => new
                {
                    FilterBankId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterBankOwners", x => new { x.FilterBankId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_FilterBankOwners_FilterBanks_FilterBankId",
                        column: x => x.FilterBankId,
                        principalTable: "FilterBanks",
                        principalColumn: "FilterBankId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterBankOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BayOwners",
                columns: table => new
                {
                    BayId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    WebUatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BayOwners", x => new { x.BayId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_BayOwners_Bays_BayId",
                        column: x => x.BayId,
                        principalTable: "Bays",
                        principalColumn: "BayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BayOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BayOwners_OwnerId",
                table: "BayOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bays_Name",
                table: "Bays",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bays_SubstationId",
                table: "Bays",
                column: "SubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bays_VoltLevelId",
                table: "Bays",
                column: "VoltLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bays_WebUatId",
                table: "Bays",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bays_BayTypeId_SourceEntityType_SourceEntityId_DestEntityId~",
                table: "Bays",
                columns: new[] { "BayTypeId", "SourceEntityType", "SourceEntityId", "DestEntityId", "DestEntityType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BayTypes_Name",
                table: "BayTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BayTypes_WebUatId",
                table: "BayTypes",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterBankOwners_OwnerId",
                table: "FilterBankOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterBanks_RegionId",
                table: "FilterBanks",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterBanks_StateId",
                table: "FilterBanks",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterBanks_VoltLevelId",
                table: "FilterBanks",
                column: "VoltLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterBanks_WebUatId",
                table: "FilterBanks",
                column: "WebUatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterBanks_SubstationId_FilterBankNumber",
                table: "FilterBanks",
                columns: new[] { "SubstationId", "FilterBankNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BayOwners");

            migrationBuilder.DropTable(
                name: "FilterBankOwners");

            migrationBuilder.DropTable(
                name: "Bays");

            migrationBuilder.DropTable(
                name: "FilterBanks");

            migrationBuilder.DropTable(
                name: "BayTypes");
        }
    }
}
