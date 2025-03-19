using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createTableHistoryPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "event_packages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sell_price = table.Column<double>(type: "float", nullable: false),
                    sale_price = table.Column<double>(type: "float", nullable: false),
                    benifit = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_packages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "event_type",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "layout",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    layout_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    layout_floor_number = table.Column<int>(type: "int", nullable: false),
                    layout_type = table.Column<int>(type: "int", nullable: false),
                    rows = table.Column<int>(type: "int", nullable: false),
                    cols = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_layout", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "event_package_registrations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    event_package_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_package_registrations", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_package_registrations_event_packages_event_package_id",
                        column: x => x.event_package_id,
                        principalTable: "event_packages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    province = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    additional_info = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    number_of_guest = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    is_open_layout = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    event_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.id);
                    table.ForeignKey(
                        name: "FK_events_event_type_event_type_id",
                        column: x => x.event_type_id,
                        principalTable: "event_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_documents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    link_document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    documents_type = table.Column<int>(type: "int", nullable: false),
                    event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_documents", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_documents_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_images",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    image_type = table.Column<int>(type: "int", maxLength: 255, nullable: false),
                    event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_images", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_images_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_invite",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    is_checked = table.Column<bool>(type: "bit", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_confirm = table.Column<bool>(type: "bit", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_invite", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_invite_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_layout",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    layout_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_layout", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_layout_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_event_layout_layout_layout_id",
                        column: x => x.layout_id,
                        principalTable: "layout",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payment_history_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_payment_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_registration_link",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_registration_link", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_registration_link_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_timeline",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    handle_by = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: false),
                    event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    timeline_type = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_timeline", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_timeline_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_seats",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    seat_label = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    row_number = table.Column<int>(type: "int", nullable: false),
                    col_number = table.Column<int>(type: "int", nullable: false),
                    seat_type = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    total_seats = table.Column<int>(type: "int", nullable: false),
                    available_seats = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_free = table.Column<bool>(type: "bit", nullable: false),
                    need_accept = table.Column<bool>(type: "bit", nullable: false),
                    event_layout_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_seats", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_seats_event_layout_event_layout_id",
                        column: x => x.event_layout_id,
                        principalTable: "event_layout",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_seats_booking",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    num_seats = table.Column<int>(type: "int", nullable: false),
                    additional_info = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    event_seat_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_seats_booking", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_seats_booking_event_seats_event_seat_id",
                        column: x => x.event_seat_id,
                        principalTable: "event_seats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_event_documents_event_id",
                table: "event_documents",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_images_event_id",
                table: "event_images",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_invite_event_id",
                table: "event_invite",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_layout_event_id",
                table: "event_layout",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_layout_layout_id",
                table: "event_layout",
                column: "layout_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_package_registrations_event_package_id",
                table: "event_package_registrations",
                column: "event_package_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_payment_event_id",
                table: "event_payment",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_registration_link_event_id",
                table: "event_registration_link",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_seats_event_layout_id",
                table: "event_seats",
                column: "event_layout_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_seats_booking_event_seat_id",
                table: "event_seats_booking",
                column: "event_seat_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_timeline_event_id",
                table: "event_timeline",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_event_type_id",
                table: "events",
                column: "event_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_documents");

            migrationBuilder.DropTable(
                name: "event_images");

            migrationBuilder.DropTable(
                name: "event_invite");

            migrationBuilder.DropTable(
                name: "event_package_registrations");

            migrationBuilder.DropTable(
                name: "event_payment");

            migrationBuilder.DropTable(
                name: "event_registration_link");

            migrationBuilder.DropTable(
                name: "event_seats_booking");

            migrationBuilder.DropTable(
                name: "event_timeline");

            migrationBuilder.DropTable(
                name: "event_packages");

            migrationBuilder.DropTable(
                name: "event_seats");

            migrationBuilder.DropTable(
                name: "event_layout");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "layout");

            migrationBuilder.DropTable(
                name: "event_type");
        }
    }
}
