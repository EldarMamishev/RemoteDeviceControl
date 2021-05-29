using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class movedefaultcommandsettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultAction",
                table: "CommandTypes");

            migrationBuilder.DropColumn(
                name: "DefaultDelta",
                table: "CommandTypes");

            migrationBuilder.AddColumn<int>(
                name: "DefaultAction",
                table: "FieldCommandTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultDelta",
                table: "FieldCommandTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultAction",
                table: "FieldCommandTypes");

            migrationBuilder.DropColumn(
                name: "DefaultDelta",
                table: "FieldCommandTypes");

            migrationBuilder.AddColumn<int>(
                name: "DefaultAction",
                table: "CommandTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultDelta",
                table: "CommandTypes",
                type: "int",
                nullable: true);
        }
    }
}
