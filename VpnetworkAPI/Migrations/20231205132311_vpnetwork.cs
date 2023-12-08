using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VpnetworkAPI.Migrations
{
    /// <inheritdoc />
    public partial class vpnetwork : Migration
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
                    ProgramGLobalMemoryThreshold = table.Column<double>(type: "float", nullable: false)
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
                name: "ProgramData",
                columns: table => new
                {
                    ProgramName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PID = table.Column<int>(type: "int", nullable: false),
                    MemoryUsage = table.Column<long>(type: "bigint", nullable: false),
                    ProgramBadCount = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramData", x => x.ProgramName);
                    table.ForeignKey(
                        name: "FK_ProgramData_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Settings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalProgramData",
                columns: table => new
                {
                    ProgramName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramLocalNetworkThreshold = table.Column<double>(type: "float", nullable: false),
                    ProgramLocalMemoryThreshold = table.Column<double>(type: "float", nullable: false),
                    SettingsUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalProgramData", x => x.ProgramName);
                    table.ForeignKey(
                        name: "FK_LocalProgramData_Settings_SettingsUserId",
                        column: x => x.SettingsUserId,
                        principalTable: "Settings",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ThresholdSettings",
                columns: table => new
                {
                    ProgramName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThresholdSetting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SettingsUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThresholdSettings", x => x.ProgramName);
                    table.ForeignKey(
                        name: "FK_ThresholdSettings_Settings_SettingsUserId",
                        column: x => x.SettingsUserId,
                        principalTable: "Settings",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalProgramData_SettingsUserId",
                table: "LocalProgramData",
                column: "SettingsUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramData_UserId",
                table: "ProgramData",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ThresholdSettings_SettingsUserId",
                table: "ThresholdSettings",
                column: "SettingsUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalData");

            migrationBuilder.DropTable(
                name: "LocalProgramData");

            migrationBuilder.DropTable(
                name: "ProgramData");

            migrationBuilder.DropTable(
                name: "ThresholdSettings");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
