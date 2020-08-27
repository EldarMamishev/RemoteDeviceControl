using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RemovedPersonalDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_PersonalDevices_PersonalDeviceId",
                table: "Connections");

            migrationBuilder.DropTable(
                name: "PersonalDevices");

            migrationBuilder.DropIndex(
                name: "IX_Connections_PersonalDeviceId",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "PersonalDeviceId",
                table: "Connections");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Connections",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Connections_PersonId",
                table: "Connections",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_PersonId",
                table: "Connections",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_PersonId",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_PersonId",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Connections");

            migrationBuilder.AddColumn<int>(
                name: "PersonalDeviceId",
                table: "Connections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonalDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalDevices_AspNetUsers_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connections_PersonalDeviceId",
                table: "Connections",
                column: "PersonalDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDevices_PersonId",
                table: "PersonalDevices",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_PersonalDevices_PersonalDeviceId",
                table: "Connections",
                column: "PersonalDeviceId",
                principalTable: "PersonalDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
