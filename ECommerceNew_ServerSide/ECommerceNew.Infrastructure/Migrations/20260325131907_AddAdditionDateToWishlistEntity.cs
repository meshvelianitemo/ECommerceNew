using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdditionDateToWishlistEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AdditonDate",
                table: "WishListItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditonDate",
                table: "WishListItems");
        }
    }
}
