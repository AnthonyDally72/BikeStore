using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeStore.Data.Migrations
{
    public partial class addstores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stores",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stores",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
