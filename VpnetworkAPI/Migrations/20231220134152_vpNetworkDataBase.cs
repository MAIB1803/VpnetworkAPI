using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VpnetworkAPI.Migrations
{
    /// <inheritdoc />
    public partial class vpNetworkDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHarmful",
                table: "LocalProgramData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHarmful",
                table: "GlobalData",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHarmful",
                table: "LocalProgramData");

            migrationBuilder.DropColumn(
                name: "IsHarmful",
                table: "GlobalData");
        }
    }
}
