using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Early_warning.Migrations.SensorData
{
    /// <inheritdoc />
    public partial class DataSensor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnomalousSensorData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Humidity = table.Column<float>(type: "real", nullable: false),
                    SmokeLevel = table.Column<float>(type: "real", nullable: false),
                    CarbonMonoxideLevel = table.Column<float>(type: "real", nullable: false),
                    FlameDetected = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnomalousSensorData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SensorsData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Humidity = table.Column<float>(type: "real", nullable: false),
                    SmokeLevel = table.Column<float>(type: "real", nullable: false),
                    CarbonMonoxideLevel = table.Column<float>(type: "real", nullable: false),
                    FlameDetected = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorsData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnomalousSensorData");

            migrationBuilder.DropTable(
                name: "SensorsData");
        }
    }
}
