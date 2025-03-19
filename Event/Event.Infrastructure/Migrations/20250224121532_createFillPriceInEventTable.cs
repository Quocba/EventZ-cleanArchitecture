using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createFillPriceInEventTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "history_price");

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "events");

            migrationBuilder.CreateTable(
                name: "history_price",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    event_package_registration_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    last_modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    old_price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history_price", x => x.id);
                    table.ForeignKey(
                        name: "FK_history_price_event_package_registrations_event_package_registration_id",
                        column: x => x.event_package_registration_id,
                        principalTable: "event_package_registrations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_history_price_event_package_registration_id",
                table: "history_price",
                column: "event_package_registration_id");
        }
    }
}
