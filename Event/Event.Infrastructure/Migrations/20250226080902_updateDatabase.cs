using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Tạo cột mới
            migrationBuilder.AddColumn<Guid>(
                name: "parent_id_new",
                table: "event_timeline",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()"); // hoặc giá trị mặc định khác phù hợp

            // 2. Di chuyển dữ liệu từ cột cũ sang cột mới (nếu có thể)
            migrationBuilder.Sql("UPDATE event_timeline SET parent_id_new = NEWID() WHERE parent_id IS NOT NULL");

            // 3. Xóa cột cũ
            migrationBuilder.DropColumn(name: "parent_id", table: "event_timeline");

            // 4. Đổi tên cột mới thành tên cột cũ
            migrationBuilder.RenameColumn(
                name: "parent_id_new",
                table: "event_timeline",
                newName: "parent_id");
        }

    }
}
