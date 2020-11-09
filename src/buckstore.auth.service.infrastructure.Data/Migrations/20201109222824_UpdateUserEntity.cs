using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.auth.service.infrastructure.Data.Migrations
{
    public partial class UpdateUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_email",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "_email",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User__email",
                table: "User",
                column: "_email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User__email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "_email",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_User_email",
                table: "User",
                column: "email",
                unique: true);
        }
    }
}
