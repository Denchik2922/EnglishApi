using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DAL.Migrations
{
    public partial class AdditionalUserFiledsForRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f183f84-82e5-4c42-8c79-b391e6127e5c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dda23481-1e77-45d9-8b2e-aea3649bae76");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "15c1a746-95eb-4408-8672-c34e23f1791b", "cec8de1e-b29b-4bf4-9f76-fdcb8819729d", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d0cf9e1-42c5-4373-a3fc-47eb241a1558", "eb7ffc5f-1a9a-4280-8b11-3b908bff056c", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15c1a746-95eb-4408-8672-c34e23f1791b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d0cf9e1-42c5-4373-a3fc-47eb241a1558");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3f183f84-82e5-4c42-8c79-b391e6127e5c", "797dd32d-b444-4d2f-95ad-9a4b64041b0f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dda23481-1e77-45d9-8b2e-aea3649bae76", "5b726df2-1d7c-4292-806a-83b2d72b20e0", "Admin", "ADMIN" });
        }
    }
}
