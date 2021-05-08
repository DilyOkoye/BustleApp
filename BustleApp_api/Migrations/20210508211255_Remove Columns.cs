using Microsoft.EntityFrameworkCore.Migrations;

namespace BustleApp_api.Migrations
{
    public partial class RemoveColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthenticationSource",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "EmailConfirmationCode",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "GoogleAuthenticatorKey",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "IsPhoneNumberConfirmed",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "IsTwoFactorEnabled",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "PasswordResetCode",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "SignInToken",
                table: "UserProfile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthenticationSource",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailConfirmationCode",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleAuthenticatorKey",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsPhoneNumberConfirmed",
                table: "UserProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsTwoFactorEnabled",
                table: "UserProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetCode",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignInToken",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
