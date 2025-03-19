using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventProduct.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createDBEventProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    province = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    additional_info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    event_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "event_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_category_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    total_price = table.Column<double>(type: "float", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateat = table.Column<DateTime>(name: "update-at", type: "datetime2", nullable: false),
                    event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_order_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sell_price = table.Column<double>(type: "float", nullable: false),
                    sale_price = table.Column<double>(type: "float", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    thumbnai_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_product_event_category_category_id",
                        column: x => x.category_id,
                        principalTable: "event_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_order_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_order_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_order_product_event_product_product_id",
                        column: x => x.product_id,
                        principalTable: "event_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_event_category_event_id",
                table: "event_category",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_order_event_id",
                table: "event_order",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_order_product_product_id",
                table: "event_order_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_product_category_id",
                table: "event_product",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_order");

            migrationBuilder.DropTable(
                name: "event_order_product");

            migrationBuilder.DropTable(
                name: "event_product");

            migrationBuilder.DropTable(
                name: "event_category");

            migrationBuilder.DropTable(
                name: "events");
        }
    }
}
