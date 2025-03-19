using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventProduct.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateTableUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_code",
                table: "events");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "events",
                type: "uniqueidentifier",
                maxLength: 20,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "events");

            migrationBuilder.AddColumn<string>(
                name: "user_code",
                table: "events",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
