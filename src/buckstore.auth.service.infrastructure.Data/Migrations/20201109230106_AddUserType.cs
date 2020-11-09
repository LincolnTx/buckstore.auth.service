using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.auth.service.infrastructure.Data.Migrations
{
    public partial class AddUserType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userType",
                table: "User",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userType",
                table: "User");
        }
    }
}
