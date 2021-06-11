using Microsoft.EntityFrameworkCore.Migrations;

namespace BustleApp_api.Migrations
{
    public partial class RemoveCOlumns2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserProfile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
