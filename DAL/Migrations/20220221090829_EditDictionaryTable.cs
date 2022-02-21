using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class EditDictionaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnglishDictionaries_AspNetUsers_UserId",
                table: "EnglishDictionaries");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94ba2739-fea3-4c54-bb8e-1d71bafb06c6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d638384-df08-4b67-86b5-e4e5d23e91e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb84d539-b80f-41f4-8337-1f02590a7623");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "afc4a156-7f70-409f-bb0e-670e7e7d6bbd", "8b410704-515c-436b-b323-4775ee047de1", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "633997a3-7d92-4e27-bac9-3aba99a44bec", "9a96ee3b-6fc5-4f21-a7ab-b5d45750d523", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b92a71f3-4ddf-42b2-a395-833f0fa9e351", "f58821d2-e1f5-4a82-afe3-c0266884a7a1", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_EnglishDictionaries_AspNetUsers_UserId",
                table: "EnglishDictionaries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnglishDictionaries_AspNetUsers_UserId",
                table: "EnglishDictionaries");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "633997a3-7d92-4e27-bac9-3aba99a44bec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afc4a156-7f70-409f-bb0e-670e7e7d6bbd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b92a71f3-4ddf-42b2-a395-833f0fa9e351");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eb84d539-b80f-41f4-8337-1f02590a7623", "d4857212-bbc5-4e53-b12b-938148385442", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9d638384-df08-4b67-86b5-e4e5d23e91e5", "d2fb5cfe-3103-41c6-87a4-6968a87206a8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "94ba2739-fea3-4c54-bb8e-1d71bafb06c6", "53529802-cd5e-4e66-998f-63ab607efb97", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_EnglishDictionaries_AspNetUsers_UserId",
                table: "EnglishDictionaries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
