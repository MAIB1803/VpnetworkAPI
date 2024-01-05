using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VpnetworkAPI.Migrations
{
    /// <inheritdoc />
    public partial class vipnetworkData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Analyses",
                table: "Analyses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Analyses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Analyses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Analyses",
                table: "Analyses",
                column: "UserId");
        }
    }
}
