using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VpnetworkAPI.Migrations
{
    /// <inheritdoc />
    public partial class vsnetworkData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_Analyses",
                table: "Analyses",
                column: "DateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Analyses",
                table: "Analyses");
        }
    }
}
