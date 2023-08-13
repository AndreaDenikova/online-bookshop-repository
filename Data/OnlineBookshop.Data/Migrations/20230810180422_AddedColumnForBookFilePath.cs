using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBookshop.Data.Migrations
{
    public partial class AddedColumnForBookFilePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookFile",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookFile",
                table: "Book");
        }
    }
}
