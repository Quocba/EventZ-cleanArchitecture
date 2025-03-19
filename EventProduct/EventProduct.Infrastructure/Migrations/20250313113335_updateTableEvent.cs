using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventProduct.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateTableEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_event_category_events_event_id",
                table: "event_category");

            migrationBuilder.DropForeignKey(
                name: "FK_event_order_events_event_id",
                table: "event_order");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropIndex(
                name: "IX_event_order_event_id",
                table: "event_order");

            migrationBuilder.DropIndex(
                name: "IX_event_category_event_id",
                table: "event_category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    additional_info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    event_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    province = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_event_order_event_id",
                table: "event_order",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_category_event_id",
                table: "event_category",
                column: "event_id");

            migrationBuilder.AddForeignKey(
                name: "FK_event_category_events_event_id",
                table: "event_category",
                column: "event_id",
                principalTable: "events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_event_order_events_event_id",
                table: "event_order",
                column: "event_id",
                principalTable: "events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
