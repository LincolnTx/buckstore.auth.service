using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.auth.service.infrastructure.Data.Migrations
{
    public partial class FixAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address__city",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address__district",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address__state",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address__street",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address__zipCode",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address__city",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Address__district",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Address__state",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Address__street",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Address__zipCode",
                table: "User");
        }
    }
}
