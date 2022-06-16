using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatChallenge.Infrastructure.Data.Migrations
{
    public partial class AlterChatroomTableAddNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Chatrooms"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Chatrooms"
            );
        }
    }
}
