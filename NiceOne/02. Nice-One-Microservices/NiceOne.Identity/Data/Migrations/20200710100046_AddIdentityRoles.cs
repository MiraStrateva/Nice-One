using Microsoft.EntityFrameworkCore.Migrations;

namespace NiceOne.Identity.Data.Migrations
{
    public partial class AddIdentityRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1e7424b1-8412-417a-b3a3-bc716690bb08", "1c8e5cd4-d01d-40e1-9a77-96fb575e5221", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a2f326bd-2bf3-4ec5-bee7-cdfa8b184477", "b4325b34-e0d6-4e45-9ade-ba99dcfaef3c", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e7424b1-8412-417a-b3a3-bc716690bb08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2f326bd-2bf3-4ec5-bee7-cdfa8b184477");
        }
    }
}
