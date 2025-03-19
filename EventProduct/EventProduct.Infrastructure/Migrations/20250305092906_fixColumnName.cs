using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventProduct.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "update-at",
                table: "event_order",
                newName: "update_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "update_at",
                table: "event_order",
                newName: "update-at");
        }
    }
}
