namespace NiceOne.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class InsertedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "79719083-5890-40db-a941-a14f6f7aa825", "3616e7f8-c203-459e-be16-5bf3d32d9f52", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c3e00b9-2d17-4c04-b7ec-96ff804eb894", "5be2559c-86bb-416d-997b-8db195fd322f", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c3e00b9-2d17-4c04-b7ec-96ff804eb894");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79719083-5890-40db-a941-a14f6f7aa825");
        }
    }
}
