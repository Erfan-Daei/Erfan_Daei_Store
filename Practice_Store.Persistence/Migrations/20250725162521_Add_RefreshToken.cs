using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Practice_Store.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_RefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a0e4c5f-cf15-40f3-94f2-17cfa8c1b855");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54ed8db0-6a0a-4753-ab18-2356585bc799");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ff3a497-32e4-4a44-9d79-9e2ee54dc441");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86113c47-77df-4e06-a2c2-5084737dd352");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3a078b7-8b3c-461d-8798-1adab33c0057");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc485dcc-7483-40e0-be2e-0eb0b7f5a3a5");

            migrationBuilder.RenameColumn(
                name: "ExpireDate",
                table: "AspNetUserTokens",
                newName: "TokenExpireDate");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUserTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpireDate",
                table: "AspNetUserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpireDate",
                table: "AspNetUserTokens");

            migrationBuilder.RenameColumn(
                name: "TokenExpireDate",
                table: "AspNetUserTokens",
                newName: "ExpireDate");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DeletedTime", "InsertTime", "IsDeleted", "Name", "NormalizedName", "UpdateTime" },
                values: new object[,]
                {
                    { "0a0e4c5f-cf15-40f3-94f2-17cfa8c1b855", null, null, new DateTime(2025, 7, 25, 15, 48, 13, 146, DateTimeKind.Utc).AddTicks(7093), false, "ProductManagement_Admin", "PRODUCTMANAGEMENT_ADMIN", null },
                    { "54ed8db0-6a0a-4753-ab18-2356585bc799", null, null, new DateTime(2025, 7, 25, 15, 48, 13, 136, DateTimeKind.Utc).AddTicks(8133), false, "Admin", "ADMIN", null },
                    { "7ff3a497-32e4-4a44-9d79-9e2ee54dc441", null, null, new DateTime(2025, 7, 25, 15, 48, 13, 146, DateTimeKind.Utc).AddTicks(7112), false, "OrderManagement_Admin", "ORDERMANAGEMENT_ADMIN", null },
                    { "86113c47-77df-4e06-a2c2-5084737dd352", null, null, new DateTime(2025, 7, 25, 15, 48, 13, 146, DateTimeKind.Utc).AddTicks(7067), false, "UserManagement_Admin", "USERMANAGEMENT_ADMIN", null },
                    { "a3a078b7-8b3c-461d-8798-1adab33c0057", null, null, new DateTime(2025, 7, 25, 15, 48, 13, 146, DateTimeKind.Utc).AddTicks(7137), false, "SiteManagement_Admin", "SITEMANAGEMENT_ADMIN", null },
                    { "cc485dcc-7483-40e0-be2e-0eb0b7f5a3a5", null, null, new DateTime(2025, 7, 25, 15, 48, 13, 146, DateTimeKind.Utc).AddTicks(6866), false, "Customer", "CUSTOMER", null }
                });
        }
    }
}
