using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedWordExample : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cb8605a-8c2b-4aeb-b20b-53f2cf4d4c58");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c57a021-3dd8-4923-8042-9c68c6c38758");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a8f0f1c-ed90-4482-93ae-9aae7037faf2");

            migrationBuilder.CreateTable(
                name: "WordExamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordExamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordExamples_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_WordExamples_WordId",
                table: "WordExamples",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordExamples");

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
                values: new object[] { "9a8f0f1c-ed90-4482-93ae-9aae7037faf2", "85ab00f0-2fb2-452c-a6d7-3688d3c96813", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c57a021-3dd8-4923-8042-9c68c6c38758", "9e89c149-11be-4d6c-bcef-c28f629f3169", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3cb8605a-8c2b-4aeb-b20b-53f2cf4d4c58", "92d32ca6-faf7-40de-a701-3843a1513168", "SuperAdmin", "SUPERADMIN" });
        }
    }
}
