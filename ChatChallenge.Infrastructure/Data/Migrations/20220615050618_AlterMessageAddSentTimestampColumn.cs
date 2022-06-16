using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatChallenge.Infrastructure.Data.Migrations
{
    public partial class AlterMessageAddSentTimestampColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SentTimestamp",
                table: "Messages",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentTimestamp",
                table: "Messages");
        }
    }
}
