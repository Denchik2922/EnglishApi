using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class EditTestResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultForMatchingTests");

            migrationBuilder.DropTable(
                name: "ResultForSpellingTests");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15c1a746-95eb-4408-8672-c34e23f1791b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d0cf9e1-42c5-4373-a3fc-47eb241a1558");

            migrationBuilder.CreateTable(
                name: "TypeOfTestings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfTestings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnglishDictionaryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TypeOfTestingId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResults_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestResults_EnglishDictionaries_EnglishDictionaryId",
                        column: x => x.EnglishDictionaryId,
                        principalTable: "EnglishDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestResults_TypeOfTestings_TypeOfTestingId",
                        column: x => x.TypeOfTestingId,
                        principalTable: "TypeOfTestings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4b60662b-ebdc-4aaf-b700-83849bd453e9", "7491c064-aa85-439a-bc19-0e177d34a156", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "635798f2-3151-4a0c-9940-0d32f4d9e487", "56492e08-73b8-483e-b058-6342a3e6b93b", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_EnglishDictionaryId",
                table: "TestResults",
                column: "EnglishDictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_TypeOfTestingId",
                table: "TestResults",
                column: "TypeOfTestingId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_UserId",
                table: "TestResults",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestResults");

            migrationBuilder.DropTable(
                name: "TypeOfTestings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b60662b-ebdc-4aaf-b700-83849bd453e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "635798f2-3151-4a0c-9940-0d32f4d9e487");

            migrationBuilder.CreateTable(
                name: "ResultForMatchingTests",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EnglishDictionaryId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultForMatchingTests", x => new { x.UserId, x.EnglishDictionaryId });
                    table.ForeignKey(
                        name: "FK_ResultForMatchingTests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultForMatchingTests_EnglishDictionaries_EnglishDictionaryId",
                        column: x => x.EnglishDictionaryId,
                        principalTable: "EnglishDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultForSpellingTests",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EnglishDictionaryId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultForSpellingTests", x => new { x.UserId, x.EnglishDictionaryId });
                    table.ForeignKey(
                        name: "FK_ResultForSpellingTests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultForSpellingTests_EnglishDictionaries_EnglishDictionaryId",
                        column: x => x.EnglishDictionaryId,
                        principalTable: "EnglishDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "15c1a746-95eb-4408-8672-c34e23f1791b", "cec8de1e-b29b-4bf4-9f76-fdcb8819729d", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d0cf9e1-42c5-4373-a3fc-47eb241a1558", "eb7ffc5f-1a9a-4280-8b11-3b908bff056c", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_ResultForMatchingTests_EnglishDictionaryId",
                table: "ResultForMatchingTests",
                column: "EnglishDictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultForSpellingTests_EnglishDictionaryId",
                table: "ResultForSpellingTests",
                column: "EnglishDictionaryId");
        }
    }
}
