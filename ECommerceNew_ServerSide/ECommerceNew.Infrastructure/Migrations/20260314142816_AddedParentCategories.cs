using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedParentCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 19);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "ParentCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "ParentCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 6,
                column: "ParentCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 7,
                column: "ParentCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 8,
                column: "ParentCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 9,
                column: "ParentCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 10,
                column: "ParentCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 11,
                column: "ParentCategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 12,
                column: "CategoryName",
                value: "Peripherals");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 13,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Monitors", 12 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 14,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Keyboards", 12 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 15,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Mice", 12 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 16,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Headsets", 12 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 17,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Speakers", 12 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 18,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Webcams & Microphones", 12 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 21,
                column: "ParentCategoryId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 22,
                column: "ParentCategoryId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 23,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Phone Accessories", 29 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 24,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Chargers & Power Banks", 29 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 25,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "PC Assembly", 30 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 26,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "PC Repair", 30 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 27,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Laptop Repair", 30 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 28,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Data Recovery", 30 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 30,
                column: "CategoryName",
                value: "Repair & Services");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "ParentCategoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "ParentCategoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 6,
                column: "ParentCategoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 7,
                column: "ParentCategoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 8,
                column: "ParentCategoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 9,
                column: "ParentCategoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 10,
                column: "ParentCategoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 11,
                column: "ParentCategoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 12,
                column: "CategoryName",
                value: "Monitors");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 13,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Keyboards", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 14,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Mice", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 15,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Headsets", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 16,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Speakers", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 17,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Webcams & Microphones", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 18,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Gaming Consoles", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 21,
                column: "ParentCategoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 22,
                column: "ParentCategoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 23,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Printers & Scanners", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 24,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "External Storage", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 25,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "USB Flash Drives", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 26,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Memory Cards", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 27,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Cables & Adapters", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 28,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Chargers & Power Banks", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 30,
                column: "CategoryName",
                value: "Repair & Technical Services");

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "CategoryName", "ParentCategoryId" },
                values: new object[] { 19, "Gaming Accessories", null });
        }
    }
}
