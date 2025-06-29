using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRWebApp.Migrations
{
    /// <inheritdoc />
    public partial class ravi1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedDate", "Description", "IsActive", "ManagerId", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 29, 16, 57, 20, 474, DateTimeKind.Utc).AddTicks(8944), "HR Department", true, null, "Human Resources" },
                    { 2, new DateTime(2025, 6, 29, 16, 57, 20, 474, DateTimeKind.Utc).AddTicks(9126), "IT Department", true, null, "Information Technology" },
                    { 3, new DateTime(2025, 6, 29, 16, 57, 20, 474, DateTimeKind.Utc).AddTicks(9128), "Finance Department", true, null, "Finance" },
                    { 4, new DateTime(2025, 6, 29, 16, 57, 20, 474, DateTimeKind.Utc).AddTicks(9129), "Operations Department", true, null, "Operations" },
                    { 5, new DateTime(2025, 6, 29, 16, 57, 20, 474, DateTimeKind.Utc).AddTicks(9131), "Marketing Department", true, null, "Marketing" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Electronic items", true, "Electronics" },
                    { 2, "Office stationery items", true, "Stationery" },
                    { 3, "Office furniture", true, "Furniture" },
                    { 4, "Software licenses", true, "Software" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
