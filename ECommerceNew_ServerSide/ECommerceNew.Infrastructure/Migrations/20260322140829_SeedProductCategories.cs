using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 30);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CategoryName",
                value: "Gaming Consoles & Accessories");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CategoryName",
                value: "PC Hardware & Components");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CategoryName",
                value: "PC & Gaming Peripherals");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Gaming PCs & Laptops", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Services", null });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "CategoryName", "ParentCategoryId" },
                values: new object[,]
                {
                    { 101, "Consoles", 1 },
                    { 102, "Gamepads & Controllers", 1 },
                    { 103, "Controller Accessories", 1 },
                    { 104, "Gift Cards / Subscriptions", 1 },
                    { 201, "Processors (CPU)", 2 },
                    { 202, "Graphics Cards (GPU)", 2 },
                    { 203, "Motherboards", 2 },
                    { 204, "Memory (RAM)", 2 },
                    { 205, "Storage (SSD / HDD / NVMe)", 2 },
                    { 206, "Power Supplies (PSU)", 2 },
                    { 207, "PC Cases", 2 },
                    { 208, "Cooling Solutions", 2 },
                    { 209, "Mining / Crypto Gear", 2 },
                    { 301, "Monitors", 3 },
                    { 302, "Keyboards", 3 },
                    { 303, "Mice", 3 },
                    { 304, "Headsets", 3 },
                    { 305, "Speakers", 3 },
                    { 306, "Other Accessories", 3 },
                    { 401, "Desktop PCs", 4 },
                    { 402, "Gaming Laptops", 4 },
                    { 403, "Laptop Accessories", 4 },
                    { 501, "PC / Laptop Assembly", 5 },
                    { 502, "PC / Laptop Repair", 5 },
                    { 503, "Upgrades & Custom Builds", 5 },
                    { 504, "Data Recovery", 5 },
                    { 505, "Consultation & Troubleshooting", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 505);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CategoryName",
                value: "Laptops");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CategoryName",
                value: "Desktop Computers");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CategoryName",
                value: "Computer Components");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Processors (CPU)", 3 });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Graphics Cards (GPU)", 3 });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "CategoryName", "ParentCategoryId" },
                values: new object[,]
                {
                    { 6, "Motherboards", 3 },
                    { 7, "Memory (RAM)", 3 },
                    { 8, "Storage (SSD / HDD / NVMe)", 3 },
                    { 9, "Power Supplies (PSU)", 3 },
                    { 10, "PC Cases", 3 },
                    { 11, "Cooling Systems", 3 },
                    { 12, "Peripherals", null },
                    { 13, "Monitors", 12 },
                    { 14, "Keyboards", 12 },
                    { 15, "Mice", 12 },
                    { 16, "Headsets", 12 },
                    { 17, "Speakers", 12 },
                    { 18, "Webcams & Microphones", 12 },
                    { 20, "Networking Equipment", null },
                    { 21, "Routers & Modems", 20 },
                    { 22, "Network Switches", 20 },
                    { 23, "Phone Accessories", 29 },
                    { 24, "Chargers & Power Banks", 29 },
                    { 25, "PC Assembly", 30 },
                    { 26, "PC Repair", 30 },
                    { 27, "Laptop Repair", 30 },
                    { 28, "Data Recovery", 30 },
                    { 29, "Smartphones & Tablets", null },
                    { 30, "Repair & Services", null }
                });
        }
    }
}
