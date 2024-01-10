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
            migrationBuilder.DropIndex(
                name: "IX_ProgramData_UserId_ProgramName",
                table: "ProgramData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlobalData",
                table: "GlobalData");

            migrationBuilder.DropColumn(
                name: "PID",
                table: "ProgramData");

            migrationBuilder.RenameTable(
                name: "GlobalData",
                newName: "GlobalProgramData");

            migrationBuilder.RenameColumn(
                name: "ThresholdSettingsDataId",
                table: "ThresholdSettings",
                newName: "ThresholdSettingDataId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassWord",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserImage",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User_Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProgramName",
                table: "ProgramData",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsHarmful",
                table: "LocalProgramData",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlobalProgramData",
                table: "GlobalProgramData",
                column: "ProgramName");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramData_UserId",
                table: "ProgramData",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProgramData_UserId",
                table: "ProgramData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlobalProgramData",
                table: "GlobalProgramData");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PassWord",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserImage",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "User_Name",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "GlobalProgramData",
                newName: "GlobalData");

            migrationBuilder.RenameColumn(
                name: "ThresholdSettingDataId",
                table: "ThresholdSettings",
                newName: "ThresholdSettingsDataId");

            migrationBuilder.AlterColumn<string>(
                name: "ProgramName",
                table: "ProgramData",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PID",
                table: "ProgramData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IsHarmful",
                table: "LocalProgramData",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlobalData",
                table: "GlobalData",
                column: "ProgramName");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramData_UserId_ProgramName",
                table: "ProgramData",
                columns: new[] { "UserId", "ProgramName" },
                unique: true);
        }
    }
}
