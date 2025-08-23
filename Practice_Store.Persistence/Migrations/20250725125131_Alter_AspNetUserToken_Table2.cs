using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Practice_Store.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Alter_AspNetUserToken_Table2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dcc38e3-a9f8-46dd-9708-48cc1b79f81d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2380660f-5ceb-41e3-803e-33ff858b6697");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2854dd10-4358-4fca-8a71-44fbb1cac1f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "296c44de-6592-4b23-82b6-77f0e9cd8fa1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d29646fd-aef0-46c4-b297-12f217b3c014");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdb5ff6c-e510-4097-aab9-7778179c3ff8");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserTokens",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "AspNetUserTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TokenId",
                table: "AspNetUserTokens",
                type: "uniqueidentifier",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "AspNetUserTokens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DeletedTime", "InsertTime", "IsDeleted", "Name", "NormalizedName", "UpdateTime" },
                values: new object[,]
                {
                    { "0dcc38e3-a9f8-46dd-9708-48cc1b79f81d", null, null, new DateTime(2025, 7, 25, 12, 40, 11, 374, DateTimeKind.Utc).AddTicks(8447), false, "OrderManagement_Admin", "ORDERMANAGEMENT_ADMIN", null },
                    { "2380660f-5ceb-41e3-803e-33ff858b6697", null, null, new DateTime(2025, 7, 25, 12, 40, 11, 374, DateTimeKind.Utc).AddTicks(8395), false, "UserManagement_Admin", "USERMANAGEMENT_ADMIN", null },
                    { "2854dd10-4358-4fca-8a71-44fbb1cac1f8", null, null, new DateTime(2025, 7, 25, 12, 40, 11, 374, DateTimeKind.Utc).AddTicks(8428), false, "ProductManagement_Admin", "PRODUCTMANAGEMENT_ADMIN", null },
                    { "296c44de-6592-4b23-82b6-77f0e9cd8fa1", null, null, new DateTime(2025, 7, 25, 12, 40, 11, 374, DateTimeKind.Utc).AddTicks(8037), false, "Customer", "CUSTOMER", null },
                    { "d29646fd-aef0-46c4-b297-12f217b3c014", null, null, new DateTime(2025, 7, 25, 12, 40, 11, 364, DateTimeKind.Utc).AddTicks(1720), false, "Admin", "ADMIN", null },
                    { "fdb5ff6c-e510-4097-aab9-7778179c3ff8", null, null, new DateTime(2025, 7, 25, 12, 40, 11, 374, DateTimeKind.Utc).AddTicks(8473), false, "SiteManagement_Admin", "SITEMANAGEMENT_ADMIN", null }
                });
        }
    }
}
