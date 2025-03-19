using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "handle_by",
                table: "event_timeline");

            migrationBuilder.CreateTable(
                name: "event_user_time_line",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    time_line_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_user_time_line", x => new { x.user_id, x.time_line_id });
                    table.ForeignKey(
                        name: "FK_event_user_time_line_event_timeline_time_line_id",
                        column: x => x.time_line_id,
                        principalTable: "event_timeline",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_event_user_time_line_time_line_id",
                table: "event_user_time_line",
                column: "time_line_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_user_time_line");

            migrationBuilder.AddColumn<string>(
                name: "handle_by",
                table: "event_timeline",
                type: "NVARCHAR(MAX)",
                nullable: false,
                defaultValue: "");
        }
    }
}
