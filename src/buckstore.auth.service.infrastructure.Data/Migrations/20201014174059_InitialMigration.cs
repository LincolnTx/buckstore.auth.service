using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.auth.service.infrastructure.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    cpf = table.Column<string>(nullable: false),
                    credCard = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    passwordSalt = table.Column<byte[]>(nullable: false),
                    surname = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
