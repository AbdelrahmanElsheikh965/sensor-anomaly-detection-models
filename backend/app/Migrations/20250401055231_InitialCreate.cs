using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Early_warning.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FloodCauses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cause = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloodCauses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(10,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeverityLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeverityLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FloodEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    SeverityId = table.Column<int>(type: "int", nullable: false),
                    CauseId = table.Column<int>(type: "int", nullable: false),
                    Impact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloodCauseId = table.Column<int>(type: "int", nullable: true),
                    LocationId1 = table.Column<int>(type: "int", nullable: true),
                    SeverityLevelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloodEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FloodEvents_FloodCauses_CauseId",
                        column: x => x.CauseId,
                        principalTable: "FloodCauses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FloodEvents_FloodCauses_FloodCauseId",
                        column: x => x.FloodCauseId,
                        principalTable: "FloodCauses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FloodEvents_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FloodEvents_Locations_LocationId1",
                        column: x => x.LocationId1,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FloodEvents_SeverityLevels_SeverityId",
                        column: x => x.SeverityId,
                        principalTable: "SeverityLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FloodEvents_SeverityLevels_SeverityLevelId",
                        column: x => x.SeverityLevelId,
                        principalTable: "SeverityLevels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FloodCauses_Cause",
                table: "FloodCauses",
                column: "Cause",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FloodEvents_CauseId",
                table: "FloodEvents",
                column: "CauseId");

            migrationBuilder.CreateIndex(
                name: "IX_FloodEvents_FloodCauseId",
                table: "FloodEvents",
                column: "FloodCauseId");

            migrationBuilder.CreateIndex(
                name: "IX_FloodEvents_LocationId",
                table: "FloodEvents",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FloodEvents_LocationId1",
                table: "FloodEvents",
                column: "LocationId1");

            migrationBuilder.CreateIndex(
                name: "IX_FloodEvents_SeverityId",
                table: "FloodEvents",
                column: "SeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_FloodEvents_SeverityLevelId",
                table: "FloodEvents",
                column: "SeverityLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_City",
                table: "Locations",
                column: "City",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeverityLevels_Level",
                table: "SeverityLevels",
                column: "Level",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FloodEvents");

            migrationBuilder.DropTable(
                name: "FloodCauses");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "SeverityLevels");
        }
    }
}
