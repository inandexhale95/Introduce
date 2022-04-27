using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Introduce.Services.Migrations
{
    public partial class Update_PasswordHasher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "RngSalt");

            migrationBuilder.AddColumn<string>(
                name: "HashedPassword",
                table: "User",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Forum",
                type: "nvarchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "RngSalt",
                table: "User",
                newName: "Password");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Forum",
                type: "nvarchar(1000",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)");
        }
    }
}
