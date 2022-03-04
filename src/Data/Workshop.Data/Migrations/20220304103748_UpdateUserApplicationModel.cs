using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Data.Migrations
{
    public partial class UpdateUserApplicationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarHash",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarHash",
                table: "AspNetUsers");
        }
    }
}
