using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Practice_Store.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SomeEntityTypeUpgrades2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c6f408d-f3b3-42da-b958-7161ac2eee92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a4cf5ea-b937-49c4-ab7f-fdc74088bafd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "579b0903-1677-494f-8647-a8e0bc5ca529");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a33a8400-95ce-4dbb-8718-762db5029bc4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dab82a8a-704b-40cb-accc-e25a57db8941");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5d149c-d0eb-4995-a529-529d1145b13c");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DeletedTime", "InsertTime", "IsDeleted", "Name", "NormalizedName", "UpdateTime" },
                values: new object[,]
                {
                    { "6dcc1724-c723-4642-ae85-2c20331dc9b5", null, null, new DateTime(2025, 7, 31, 19, 2, 11, 382, DateTimeKind.Utc).AddTicks(4004), false, "OrderManagement_Admin", "ORDERMANAGEMENT_ADMIN", null },
                    { "6f522716-1b81-46be-954c-456b0d072b13", null, null, new DateTime(2025, 7, 31, 19, 2, 11, 382, DateTimeKind.Utc).AddTicks(3552), false, "Customer", "CUSTOMER", null },
                    { "b86efbb1-9e79-4fdc-85ee-dda1beb02b40", null, null, new DateTime(2025, 7, 31, 19, 2, 11, 367, DateTimeKind.Utc).AddTicks(8283), false, "Admin", "ADMIN", null },
                    { "b9c88ef1-e746-4f19-be92-3eb72ea75f2a", null, null, new DateTime(2025, 7, 31, 19, 2, 11, 382, DateTimeKind.Utc).AddTicks(4050), false, "SiteManagement_Admin", "SITEMANAGEMENT_ADMIN", null },
                    { "bb031313-f014-4d63-b5ec-3f50ce793eb5", null, null, new DateTime(2025, 7, 31, 19, 2, 11, 382, DateTimeKind.Utc).AddTicks(3915), false, "UserManagement_Admin", "USERMANAGEMENT_ADMIN", null },
                    { "d9f96f12-8e66-4073-9fa2-b6712eea751a", null, null, new DateTime(2025, 7, 31, 19, 2, 11, 382, DateTimeKind.Utc).AddTicks(3956), false, "ProductManagement_Admin", "PRODUCTMANAGEMENT_ADMIN", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6dcc1724-c723-4642-ae85-2c20331dc9b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f522716-1b81-46be-954c-456b0d072b13");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b86efbb1-9e79-4fdc-85ee-dda1beb02b40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9c88ef1-e746-4f19-be92-3eb72ea75f2a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb031313-f014-4d63-b5ec-3f50ce793eb5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9f96f12-8e66-4073-9fa2-b6712eea751a");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DeletedTime", "InsertTime", "IsDeleted", "Name", "NormalizedName", "UpdateTime" },
                values: new object[,]
                {
                    { "1c6f408d-f3b3-42da-b958-7161ac2eee92", null, null, new DateTime(2025, 7, 31, 18, 57, 0, 560, DateTimeKind.Utc).AddTicks(2546), false, "OrderManagement_Admin", "ORDERMANAGEMENT_ADMIN", null },
                    { "3a4cf5ea-b937-49c4-ab7f-fdc74088bafd", null, null, new DateTime(2025, 7, 31, 18, 57, 0, 542, DateTimeKind.Utc).AddTicks(8173), false, "Admin", "ADMIN", null },
                    { "579b0903-1677-494f-8647-a8e0bc5ca529", null, null, new DateTime(2025, 7, 31, 18, 57, 0, 560, DateTimeKind.Utc).AddTicks(2583), false, "SiteManagement_Admin", "SITEMANAGEMENT_ADMIN", null },
                    { "a33a8400-95ce-4dbb-8718-762db5029bc4", null, null, new DateTime(2025, 7, 31, 18, 57, 0, 560, DateTimeKind.Utc).AddTicks(2474), false, "UserManagement_Admin", "USERMANAGEMENT_ADMIN", null },
                    { "dab82a8a-704b-40cb-accc-e25a57db8941", null, null, new DateTime(2025, 7, 31, 18, 57, 0, 560, DateTimeKind.Utc).AddTicks(2510), false, "ProductManagement_Admin", "PRODUCTMANAGEMENT_ADMIN", null },
                    { "fa5d149c-d0eb-4995-a529-529d1145b13c", null, null, new DateTime(2025, 7, 31, 18, 57, 0, 560, DateTimeKind.Utc).AddTicks(1817), false, "Customer", "CUSTOMER", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
