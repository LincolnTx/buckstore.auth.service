using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.auth.service.infrastructure.Data.Migrations
{
    public partial class NewRefreshTokenEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "refreshTokenId",
                table: "User",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserRefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    userRefreshToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshToken", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_refreshTokenId",
                table: "User",
                column: "refreshTokenId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserRefreshToken_refreshTokenId",
                table: "User",
                column: "refreshTokenId",
                principalTable: "UserRefreshToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserRefreshToken_refreshTokenId",
                table: "User");

            migrationBuilder.DropTable(
                name: "UserRefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_User_refreshTokenId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "refreshTokenId",
                table: "User");
        }
    }
}
