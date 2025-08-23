using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Practice_Store.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Alter_AspNetUserToken_Table3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1af5b189-d0f0-4266-8b62-a0474da295be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26e88cdb-5148-487e-96f6-1b21e2015584");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4aadd1ed-d0f6-4249-a9eb-3d5ab01bd181");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6dc0fec3-5328-487f-9094-859cb6fd2f71");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78bf5367-69f7-465b-9b97-da325d6e0a9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "797e9ff6-7e7b-46e5-a458-c6cf9ecac718");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserTokens");

            migrationBuilder.AlterColumn<Guid>(
                name: "TokenId",
                table: "AspNetUserTokens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpireDate",
                table: "AspNetUserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "TokenId", "UserId", "LoginProvider" });

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_LoginProvider",
                table: "AspNetUserTokens",
                column: "LoginProvider");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_UserId",
                table: "AspNetUserTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserTokens_LoginProvider",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserTokens_UserId",
                table: "AspNetUserTokens");

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpireDate",
                table: "AspNetUserTokens",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "TokenId",
                table: "AspNetUserTokens",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserTokens",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DeletedTime", "InsertTime", "IsDeleted", "Name", "NormalizedName", "UpdateTime" },
                values: new object[,]
                {
                    { "1af5b189-d0f0-4266-8b62-a0474da295be", null, null, new DateTime(2025, 7, 25, 12, 51, 30, 175, DateTimeKind.Utc).AddTicks(3897), false, "UserManagement_Admin", "USERMANAGEMENT_ADMIN", null },
                    { "26e88cdb-5148-487e-96f6-1b21e2015584", null, null, new DateTime(2025, 7, 25, 12, 51, 30, 175, DateTimeKind.Utc).AddTicks(3672), false, "Customer", "CUSTOMER", null },
                    { "4aadd1ed-d0f6-4249-a9eb-3d5ab01bd181", null, null, new DateTime(2025, 7, 25, 12, 51, 30, 175, DateTimeKind.Utc).AddTicks(3918), false, "ProductManagement_Admin", "PRODUCTMANAGEMENT_ADMIN", null },
                    { "6dc0fec3-5328-487f-9094-859cb6fd2f71", null, null, new DateTime(2025, 7, 25, 12, 51, 30, 175, DateTimeKind.Utc).AddTicks(3965), false, "SiteManagement_Admin", "SITEMANAGEMENT_ADMIN", null },
                    { "78bf5367-69f7-465b-9b97-da325d6e0a9e", null, null, new DateTime(2025, 7, 25, 12, 51, 30, 164, DateTimeKind.Utc).AddTicks(3994), false, "Admin", "ADMIN", null },
                    { "797e9ff6-7e7b-46e5-a458-c6cf9ecac718", null, null, new DateTime(2025, 7, 25, 12, 51, 30, 175, DateTimeKind.Utc).AddTicks(3935), false, "OrderManagement_Admin", "ORDERMANAGEMENT_ADMIN", null }
                });
        }
    }
}
