using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.auth.service.infrastructure.Data.Migrations
{
    public partial class AddUniqueProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_cpf",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User__cpf",
                table: "User",
                column: "_cpf",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User__cpf",
                table: "User");

            migrationBuilder.DropColumn(
                name: "_cpf",
                table: "User");
        }
    }
}
