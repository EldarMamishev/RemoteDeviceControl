using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PossibleValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "FieldPossibleValues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "FieldPossibleValues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "FieldPossibleValues");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "FieldPossibleValues");
        }
    }
}
