using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixedProductCategoriesSecondVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreInfo",
                table: "StoreInfo");

            migrationBuilder.RenameTable(
                name: "StoreInfo",
                newName: "StoreInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreInfos",
                table: "StoreInfos",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreInfos",
                table: "StoreInfos");

            migrationBuilder.RenameTable(
                name: "StoreInfos",
                newName: "StoreInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreInfo",
                table: "StoreInfo",
                column: "Id");
        }
    }
}
