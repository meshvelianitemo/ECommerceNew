using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixedProductCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryId",
                table: "ProductCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StoreInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkingHours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreInfo", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Laptops", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Desktop Computers", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Computer Components", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Processors (CPU)", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Graphics Cards (GPU)", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 6,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Motherboards", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 7,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Memory (RAM)", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 8,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Storage (SSD / HDD / NVMe)", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 9,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Power Supplies (PSU)", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 10,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "PC Cases", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 11,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Cooling Systems", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 12,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Monitors", null });

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
                keyValue: 19,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Gaming Accessories", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 20,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Networking Equipment", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 21,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Routers & Modems", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 22,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Network Switches", null });

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
                keyValue: 29,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Smartphones & Tablets", null });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 30,
                columns: new[] { "CategoryName", "ParentCategoryId" },
                values: new object[] { "Repair & Technical Services", null });

            migrationBuilder.InsertData(
                table: "StoreInfo",
                columns: new[] { "Id", "Address", "Email", "LogoUrl", "Name", "Phone", "WorkingHours" },
                values: new object[] { 1, "123 Tech Street, Tbilisi, Georgia", "temomeshveliani777@gmail.com", "/images/logo.png", "EkkoShop", "+995 599 931 611", "Mon-Fri 9:00-18:00" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreInfo");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "ProductCategories");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CategoryName",
                value: "Buy on Installments");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CategoryName",
                value: "Large Domestic Appliances");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CategoryName",
                value: "Mobile Phones");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CategoryName",
                value: "Sales");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "CategoryName",
                value: "Laptops");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 6,
                column: "CategoryName",
                value: "Game Consoles");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 7,
                column: "CategoryName",
                value: "Art and Collection");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 8,
                column: "CategoryName",
                value: "Desktop Computers");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 9,
                column: "CategoryName",
                value: "Services");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 10,
                column: "CategoryName",
                value: "Training Inventory");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 11,
                column: "CategoryName",
                value: "Bicycles");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 12,
                column: "CategoryName",
                value: "Hiking and Tourism");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 13,
                column: "CategoryName",
                value: "Computer Hardware");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 14,
                column: "CategoryName",
                value: "Bed");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 15,
                column: "CategoryName",
                value: "Watches & Accessories");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 16,
                column: "CategoryName",
                value: "Outlet");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 17,
                column: "CategoryName",
                value: "Pets");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 18,
                column: "CategoryName",
                value: "Kids");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 19,
                column: "CategoryName",
                value: "TV");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 20,
                column: "CategoryName",
                value: "Small Household Appliances");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 21,
                column: "CategoryName",
                value: "Books");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 22,
                column: "CategoryName",
                value: "Kitchen");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 23,
                column: "CategoryName",
                value: "Loud Speakers");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 24,
                column: "CategoryName",
                value: "Headphones");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 25,
                column: "CategoryName",
                value: "Hunting Equipment");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 26,
                column: "CategoryName",
                value: "Monitors");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 27,
                column: "CategoryName",
                value: "Shoes");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 28,
                column: "CategoryName",
                value: "Industrial Equipment");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 29,
                column: "CategoryName",
                value: "Music");

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 30,
                column: "CategoryName",
                value: "Super VIP");
        }
    }
}
