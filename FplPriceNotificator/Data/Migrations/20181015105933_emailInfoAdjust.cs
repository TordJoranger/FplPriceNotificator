using Microsoft.EntityFrameworkCore.Migrations;

namespace FplPriceNotificator.Data.Migrations
{
    public partial class emailInfoAdjust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailID",
                table: "EmailInfo",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "EmailInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EmailInfo",
                newName: "EmailID");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "EmailInfo",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
