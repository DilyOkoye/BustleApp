using Microsoft.EntityFrameworkCore.Migrations;

namespace BustleApp_api.Migrations
{
    public partial class NewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "UserProfile",
                newName: "userPassword");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "UserProfile",
                newName: "userEmail");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "businessName",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "industryType",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userAddress",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "State",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "businessName",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "industryType",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "userAddress",
                table: "UserProfile");

            migrationBuilder.RenameColumn(
                name: "userPassword",
                table: "UserProfile",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "userEmail",
                table: "UserProfile",
                newName: "EmailAddress");
        }
    }
}
