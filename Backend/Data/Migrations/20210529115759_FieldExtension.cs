using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class FieldExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommandType",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "MongoCommandDetailsId",
                table: "Commands");

            migrationBuilder.AddColumn<int>(
                name: "CommandTypeId",
                table: "Commands",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessGroupId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccessGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessIdentifiers = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommandTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DefaultAction = table.Column<int>(nullable: true),
                    DefaultDelta = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceFieldCommands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandId = table.Column<int>(nullable: true),
                    DeviceFieldId = table.Column<int>(nullable: true),
                    OldValue = table.Column<string>(nullable: true),
                    NewValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceFieldCommands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceFieldCommands_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceFieldCommands_DeviceFields_DeviceFieldId",
                        column: x => x.DeviceFieldId,
                        principalTable: "DeviceFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FieldPossibleValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldPossibleValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldPossibleValues_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FieldCommandTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandTypeId = table.Column<int>(nullable: true),
                    FieldId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldCommandTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldCommandTypes_CommandTypes_CommandTypeId",
                        column: x => x.CommandTypeId,
                        principalTable: "CommandTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldCommandTypes_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_CommandTypeId",
                table: "Commands",
                column: "CommandTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AccessGroupId",
                table: "AspNetUsers",
                column: "AccessGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceFieldCommands_CommandId",
                table: "DeviceFieldCommands",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceFieldCommands_DeviceFieldId",
                table: "DeviceFieldCommands",
                column: "DeviceFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldCommandTypes_CommandTypeId",
                table: "FieldCommandTypes",
                column: "CommandTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldCommandTypes_FieldId",
                table: "FieldCommandTypes",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldPossibleValues_FieldId",
                table: "FieldPossibleValues",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AccessGroups_AccessGroupId",
                table: "AspNetUsers",
                column: "AccessGroupId",
                principalTable: "AccessGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_CommandTypes_CommandTypeId",
                table: "Commands",
                column: "CommandTypeId",
                principalTable: "CommandTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AccessGroups_AccessGroupId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Commands_CommandTypes_CommandTypeId",
                table: "Commands");

            migrationBuilder.DropTable(
                name: "AccessGroups");

            migrationBuilder.DropTable(
                name: "DeviceFieldCommands");

            migrationBuilder.DropTable(
                name: "FieldCommandTypes");

            migrationBuilder.DropTable(
                name: "FieldPossibleValues");

            migrationBuilder.DropTable(
                name: "CommandTypes");

            migrationBuilder.DropIndex(
                name: "IX_Commands_CommandTypeId",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AccessGroupId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CommandTypeId",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "AccessGroupId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CommandType",
                table: "Commands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MongoCommandDetailsId",
                table: "Commands",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
