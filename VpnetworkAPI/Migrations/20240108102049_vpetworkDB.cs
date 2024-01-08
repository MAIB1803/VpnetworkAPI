using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VpnetworkAPI.Migrations
{
    /// <inheritdoc />
    public partial class vpetworkDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalData",
                columns: table => new
                {
                    ProgramName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProgramGlobalNetworkThreshold = table.Column<double>(type: "float", nullable: false),
                    ProgramGLobalMemoryThreshold = table.Column<double>(type: "float", nullable: false),
                    IsHarmful = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalData", x => x.ProgramName);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Analyses",
                columns: table => new
                {
                    AnalysisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemoryUsage = table.Column<long>(type: "bigint", nullable: false),
                    NetworkUsage = table.Column<double>(type: "float", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyses", x => x.AnalysisId);
                    table.ForeignKey(
                        name: "FK_Analyses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalProgramData",
                columns: table => new
                {
                    LocalProgramDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramLocalNetworkThreshold = table.Column<double>(type: "float", nullable: false),
                    ProgramLocalMemoryThreshold = table.Column<double>(type: "float", nullable: false),
                    IsHarmful = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalProgramData", x => x.LocalProgramDataId);
                    table.ForeignKey(
                        name: "FK_LocalProgramData_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramData",
                columns: table => new
                {
                    ProgramDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PID = table.Column<int>(type: "int", nullable: false),
                    MemoryUsage = table.Column<long>(type: "bigint", nullable: false),
                    NetworkUsage = table.Column<double>(type: "float", nullable: false),
                    ProgramBadCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramData", x => x.ProgramDataId);
                    table.ForeignKey(
                        name: "FK_ProgramData_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThresholdSettings",
                columns: table => new
                {
                    ThresholdSettingsDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThresholdSetting = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThresholdSettings", x => x.ThresholdSettingsDataId);
                    table.ForeignKey(
                        name: "FK_ThresholdSettings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_UserId",
                table: "Analyses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalProgramData_UserId",
                table: "LocalProgramData",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramData_UserId_ProgramName",
                table: "ProgramData",
                columns: new[] { "UserId", "ProgramName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThresholdSettings_UserId",
                table: "ThresholdSettings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analyses");

            migrationBuilder.DropTable(
                name: "GlobalData");

            migrationBuilder.DropTable(
                name: "LocalProgramData");

            migrationBuilder.DropTable(
                name: "ProgramData");

            migrationBuilder.DropTable(
                name: "ThresholdSettings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
