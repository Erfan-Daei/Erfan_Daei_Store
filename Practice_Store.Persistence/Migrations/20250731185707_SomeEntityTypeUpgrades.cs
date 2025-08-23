using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Practice_Store.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SomeEntityTypeUpgrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07fdba47-772b-4ee4-8f1b-bd7ad4775166");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c551958-a993-41bd-9589-b952d34f430c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a92bce71-1ee5-4c36-b983-68e33ae6af1f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ccc08f28-8737-49f9-bf54-35eb893f30a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3c5cf55-95fd-4c6c-bd5e-1a40b5d7fb9d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f696eecc-6e4e-4802-8b72-6941ce8e2b87");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "OrderRequestExtraInfos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<long>(
                name: "Mobile",
                table: "OrderRequestExtraInfos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DeletedTime", "InsertTime", "IsDeleted", "Name", "NormalizedName", "UpdateTime" },
                values: new object[,]
                {
                    { "07fdba47-772b-4ee4-8f1b-bd7ad4775166", null, null, new DateTime(2025, 7, 25, 16, 25, 20, 290, DateTimeKind.Utc).AddTicks(3263), false, "ProductManagement_Admin", "PRODUCTMANAGEMENT_ADMIN", null },
                    { "7c551958-a993-41bd-9589-b952d34f430c", null, null, new DateTime(2025, 7, 25, 16, 25, 20, 290, DateTimeKind.Utc).AddTicks(3076), false, "Customer", "CUSTOMER", null },
                    { "a92bce71-1ee5-4c36-b983-68e33ae6af1f", null, null, new DateTime(2025, 7, 25, 16, 25, 20, 280, DateTimeKind.Utc).AddTicks(1485), false, "Admin", "ADMIN", null },
                    { "ccc08f28-8737-49f9-bf54-35eb893f30a6", null, null, new DateTime(2025, 7, 25, 16, 25, 20, 290, DateTimeKind.Utc).AddTicks(3277), false, "OrderManagement_Admin", "ORDERMANAGEMENT_ADMIN", null },
                    { "e3c5cf55-95fd-4c6c-bd5e-1a40b5d7fb9d", null, null, new DateTime(2025, 7, 25, 16, 25, 20, 290, DateTimeKind.Utc).AddTicks(3303), false, "SiteManagement_Admin", "SITEMANAGEMENT_ADMIN", null },
                    { "f696eecc-6e4e-4802-8b72-6941ce8e2b87", null, null, new DateTime(2025, 7, 25, 16, 25, 20, 290, DateTimeKind.Utc).AddTicks(3243), false, "UserManagement_Admin", "USERMANAGEMENT_ADMIN", null }
                });
        }
    }
}
