using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.auth.service.infrastructure.Data.Migrations
{
    public partial class RemoveIndexForTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User__cpf",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User__email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "_cpf",
                table: "User");

            migrationBuilder.DropColumn(
                name: "_email",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_cpf",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_email",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User__cpf",
                table: "User",
                column: "_cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User__email",
                table: "User",
                column: "_email",
                unique: true);
        }
    }
}
