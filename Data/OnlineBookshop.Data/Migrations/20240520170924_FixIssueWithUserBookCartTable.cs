using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBookshop.Data.Migrations
{
    public partial class FixIssueWithUserBookCartTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBookCart_UserInfo_UsertId",
                table: "UserBookCart");

            migrationBuilder.DropIndex(
                name: "IX_UserBookCart_UsertId",
                table: "UserBookCart");

            migrationBuilder.DropColumn(
                name: "UsertId",
                table: "UserBookCart");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookCart_UserInfo_UserId",
                table: "UserBookCart",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBookCart_UserInfo_UserId",
                table: "UserBookCart");

            migrationBuilder.AddColumn<string>(
                name: "UsertId",
                table: "UserBookCart",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBookCart_UsertId",
                table: "UserBookCart",
                column: "UsertId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookCart_UserInfo_UsertId",
                table: "UserBookCart",
                column: "UsertId",
                principalTable: "UserInfo",
                principalColumn: "Id");
        }
    }
}
