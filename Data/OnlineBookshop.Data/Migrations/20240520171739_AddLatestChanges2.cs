using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBookshop.Data.Migrations
{
    public partial class AddLatestChanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBookCart_UserInfo_UserId",
                table: "UserBookCart");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookCart_AspNetUsers_UserId",
                table: "UserBookCart",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBookCart_AspNetUsers_UserId",
                table: "UserBookCart");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookCart_UserInfo_UserId",
                table: "UserBookCart",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
