using Microsoft.EntityFrameworkCore.Migrations;

namespace datingapp.api.Migrations
{
    public partial class ChangeUserFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Id");

            
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "ID");

        }
    }
}
