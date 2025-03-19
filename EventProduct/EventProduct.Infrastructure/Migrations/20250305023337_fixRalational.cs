using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventProduct.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixRalational : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "event_order_id",
                table: "event_order_product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "payment_history_id",
                table: "event_order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_event_order_product_event_order_id",
                table: "event_order_product",
                column: "event_order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_event_order_product_event_order_event_order_id",
                table: "event_order_product",
                column: "event_order_id",
                principalTable: "event_order",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_event_order_product_event_order_event_order_id",
                table: "event_order_product");

            migrationBuilder.DropIndex(
                name: "IX_event_order_product_event_order_id",
                table: "event_order_product");

            migrationBuilder.DropColumn(
                name: "event_order_id",
                table: "event_order_product");

            migrationBuilder.DropColumn(
                name: "payment_history_id",
                table: "event_order");
        }
    }
}
