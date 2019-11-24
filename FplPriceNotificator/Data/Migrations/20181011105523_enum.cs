using Microsoft.EntityFrameworkCore.Migrations;

namespace FplPriceNotificator.Data.Migrations
{
    public partial class @enum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Threshold",
                table: "EmailInfo",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Threshold",
                table: "EmailInfo",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
