using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.auth.service.infrastructure.Data.Migrations
{
    public partial class AddRefreshTokenTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserRefreshToken_userId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_userId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "User");

            migrationBuilder.AddColumn<Guid>(
                name: "refreshTokenId",
                table: "User",
                nullable: true);

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

            migrationBuilder.DropIndex(
                name: "IX_User_refreshTokenId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "refreshTokenId",
                table: "User");

            migrationBuilder.AddColumn<Guid>(
                name: "userId",
                table: "User",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_userId",
                table: "User",
                column: "userId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserRefreshToken_userId",
                table: "User",
                column: "userId",
                principalTable: "UserRefreshToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
