using Microsoft.EntityFrameworkCore.Migrations;

namespace FplPriceNotificator.Data.Migrations
{
    public partial class Entry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Entry",
                table: "EmailInfo",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Entry",
                table: "EmailInfo");
        }
    }
}
