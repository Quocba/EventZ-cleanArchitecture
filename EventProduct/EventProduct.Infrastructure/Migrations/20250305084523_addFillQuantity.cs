using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventProduct.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addFillQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "event_order_product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "event_order_product");
        }
    }
}
