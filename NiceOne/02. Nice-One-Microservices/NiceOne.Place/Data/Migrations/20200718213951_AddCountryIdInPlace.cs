using Microsoft.EntityFrameworkCore.Migrations;

namespace NiceOne.Place.Data.Migrations
{
    public partial class AddCountryIdInPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Places",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Places");
        }
    }
}
