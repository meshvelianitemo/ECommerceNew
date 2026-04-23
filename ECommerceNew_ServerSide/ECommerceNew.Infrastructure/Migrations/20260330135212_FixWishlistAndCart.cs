using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixWishlistAndCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditonDate",
                table: "WishListItems",
                newName: "AdditionDate");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "AdditionDate",
                table: "WishListItems",
                newName: "AdditonDate");
        }
    }
}
