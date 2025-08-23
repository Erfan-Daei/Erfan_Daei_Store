using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Practice_Store.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Alter_AspNetUserToken_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e5da71b-b386-4e94-ad57-3410f7d64332");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7273cfdb-6866-4dfc-ac94-f5c1563f7e27");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8400ae9e-25e2-42ff-831c-dad905b4adf8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90d23e28-66f0-4a68-8498-cb7d6ada0915");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98507c45-f3e4-4740-b89f-e9abc85d205f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebe65a86-4bec-4926-abd7-4e74095e4e80");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DeletedTime", "InsertTime", "IsDeleted", "Name", "NormalizedName", "UpdateTime" },
                values: new object[,]
                {
                    { "5e5da71b-b386-4e94-ad57-3410f7d64332", null, null, new DateTime(2025, 4, 7, 9, 34, 23, 280, DateTimeKind.Utc).AddTicks(8442), false, "Customer", "CUSTOMER", null },
                    { "7273cfdb-6866-4dfc-ac94-f5c1563f7e27", null, null, new DateTime(2025, 4, 7, 9, 34, 23, 280, DateTimeKind.Utc).AddTicks(8605), false, "UserManagement_Admin", "USERMANAGEMENT_ADMIN", null },
                    { "8400ae9e-25e2-42ff-831c-dad905b4adf8", null, null, new DateTime(2025, 4, 7, 9, 34, 23, 280, DateTimeKind.Utc).AddTicks(8699), false, "OrderManagement_Admin", "ORDERMANAGEMENT_ADMIN", null },
                    { "90d23e28-66f0-4a68-8498-cb7d6ada0915", null, null, new DateTime(2025, 4, 7, 9, 34, 23, 280, DateTimeKind.Utc).AddTicks(8684), false, "ProductManagement_Admin", "PRODUCTMANAGEMENT_ADMIN", null },
                    { "98507c45-f3e4-4740-b89f-e9abc85d205f", null, null, new DateTime(2025, 4, 7, 9, 34, 23, 280, DateTimeKind.Utc).AddTicks(8722), false, "SiteManagement_Admin", "SITEMANAGEMENT_ADMIN", null },
                    { "ebe65a86-4bec-4926-abd7-4e74095e4e80", null, null, new DateTime(2025, 4, 7, 9, 34, 23, 271, DateTimeKind.Utc).AddTicks(3717), false, "Admin", "ADMIN", null }
                });
        }
    }
}
