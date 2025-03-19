using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDBUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "event_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_email_confirmed = table.Column<bool>(type: "bit", nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    is_phone_confirmed = table.Column<bool>(type: "bit", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    create_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    last_modified_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_users_create_by",
                        column: x => x.create_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "email_confirmation_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    token = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    expiration_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_confirmation_tokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_email_confirmation_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refesh_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    token = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    expiration_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refesh_tokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_refesh_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_event",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    event_role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_event", x => new { x.user_id, x.event_id });
                    table.ForeignKey(
                        name: "FK_user_event_event_roles_event_role_id",
                        column: x => x.event_role_id,
                        principalTable: "event_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_event_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.role_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "event_roles",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { new Guid("1348a93d-b890-434f-9d55-829e5e8f3a8b"), "Khách hàng tham gia bằng link", "RegistrationLinkGuest" },
                    { new Guid("7fd3c0a5-6883-4d0d-949a-1300d101e71b"), "Khách hàng miễn phí", "FreeGuest" },
                    { new Guid("80ae6925-6883-4d0d-949a-e3d35994e410"), "Nhân viên", "Employee" },
                    { new Guid("9345487f-f275-46f0-8751-e3d35994e410"), "Khách hàng trả phí", "PaidGuest" },
                    { new Guid("c57b6454-603a-4597-8200-e09f8ca795fe"), "Khách hàng bình thường", "Guest" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { new Guid("1348a93d-b890-434f-9d55-829e5e8f3a8b"), "Staff", "Staff" },
                    { new Guid("1d39a1c4-5663-4767-80a8-f56c869be5c8"), "Admin", "Admin" },
                    { new Guid("80ae6925-a266-455c-9d0c-dc4cd3205ba4"), "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "create_at", "create_by", "email", "first_name", "gender", "is_active", "is_email_confirmed", "is_phone_confirmed", "last_modified_at", "last_name", "password_hash", "phone", "username" },
                values: new object[,]
                {
                    { new Guid("4d62d1cc-dfde-4580-86e4-d455be5e6c40"), null, new DateTime(2025, 3, 7, 15, 51, 59, 840, DateTimeKind.Utc).AddTicks(5462), null, "admin@gmail.com", "Admin", 0, true, true, false, new DateTime(2025, 3, 7, 15, 51, 59, 840, DateTimeKind.Utc).AddTicks(5487), "Admin", "$2a$11$8gbDuFGvnYI2pw6/jxEp7eqaFtw6A4vnVkp6HRlkyluB1xFguyuxG", "0386040060", "admin" },
                    { new Guid("69b15efb-62e2-48d8-976a-8edd8f6dd658"), null, new DateTime(2025, 3, 7, 15, 51, 59, 962, DateTimeKind.Utc).AddTicks(2706), null, "staff@gmail.com", "Staff", 0, true, true, false, new DateTime(2025, 3, 7, 15, 51, 59, 962, DateTimeKind.Utc).AddTicks(2712), "Staff", "$2a$11$KE3LwDk8YA5YvAPMDVOgtOE0u4S9rGMfBrzi72zrI5TojkqfANrWO", "0372599558", "staff" },
                    { new Guid("a7ddfb9a-7c54-4444-82af-558936266f97"), null, new DateTime(2025, 3, 7, 15, 52, 0, 85, DateTimeKind.Utc).AddTicks(2599), null, "user@gmail.com", "User", 0, true, true, false, new DateTime(2025, 3, 7, 15, 52, 0, 85, DateTimeKind.Utc).AddTicks(2610), "User", "$2a$11$s8mhhKWJ9EdDpW6YtoeRd.kPKojtFkFuyy1fDW1CWfgq/nBpu99.C", "0372599559", "user" }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "role_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("1348a93d-b890-434f-9d55-829e5e8f3a8b"), new Guid("69b15efb-62e2-48d8-976a-8edd8f6dd658") },
                    { new Guid("1d39a1c4-5663-4767-80a8-f56c869be5c8"), new Guid("4d62d1cc-dfde-4580-86e4-d455be5e6c40") },
                    { new Guid("80ae6925-a266-455c-9d0c-dc4cd3205ba4"), new Guid("a7ddfb9a-7c54-4444-82af-558936266f97") }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "create_at", "create_by", "email", "first_name", "gender", "is_active", "is_email_confirmed", "is_phone_confirmed", "last_modified_at", "last_name", "password_hash", "phone", "username" },
                values: new object[,]
                {
                    { new Guid("43d12322-38c7-4f89-a95b-b941dfa9564c"), null, new DateTime(2025, 3, 7, 15, 52, 0, 328, DateTimeKind.Utc).AddTicks(6808), new Guid("4d62d1cc-dfde-4580-86e4-d455be5e6c40"), "guest2@gmail.com", "Guest", 1, true, true, true, new DateTime(2025, 3, 7, 15, 52, 0, 328, DateTimeKind.Utc).AddTicks(6814), "2", "$2a$11$sDGww/6SPNNhn3oNNqvAZeU94zgTnEvR3aU5Edoqe3jXevFD1CDVe", "0372599558", "guest2" },
                    { new Guid("97775ef6-6c52-42ff-8378-0321b5caef4f"), null, new DateTime(2025, 3, 7, 15, 52, 0, 451, DateTimeKind.Utc).AddTicks(4479), new Guid("4d62d1cc-dfde-4580-86e4-d455be5e6c40"), "guest3@gmail.com", "Guest", 1, true, true, true, new DateTime(2025, 3, 7, 15, 52, 0, 451, DateTimeKind.Utc).AddTicks(4527), "3", "$2a$11$0Fn0RItL/DsLsp.2r1TkIuCCz2t5p0PG69DXErXohCdVWu8oBy/JC", "0372599559", "guest3" },
                    { new Guid("b117f14c-2d22-4bc6-95b5-8f5dff43a0e5"), null, new DateTime(2025, 3, 7, 15, 52, 0, 207, DateTimeKind.Utc).AddTicks(8134), new Guid("4d62d1cc-dfde-4580-86e4-d455be5e6c40"), "guest1@gmail.com", "Guest", 1, true, true, true, new DateTime(2025, 3, 7, 15, 52, 0, 207, DateTimeKind.Utc).AddTicks(8144), "1", "$2a$11$YQEjvvfoJJpqnVQz6M1h0.hDv0YkD8UwC8vLexe3HN/RUVNcpYxmi", "0386040060", "guest1" }
                });

            migrationBuilder.InsertData(
                table: "user_event",
                columns: new[] { "event_id", "user_id", "event_role_id" },
                values: new object[,]
                {
                    { new Guid("091cfb4e-f4de-422b-8e2c-992c67a46d7e"), new Guid("43d12322-38c7-4f89-a95b-b941dfa9564c"), new Guid("1348a93d-b890-434f-9d55-829e5e8f3a8b") },
                    { new Guid("1dbf03f8-4ad9-47bd-b1bf-d0d073e8dba7"), new Guid("97775ef6-6c52-42ff-8378-0321b5caef4f"), new Guid("9345487f-f275-46f0-8751-e3d35994e410") },
                    { new Guid("98154ede-c3ff-49de-8e0f-3b2f0af2aa6a"), new Guid("b117f14c-2d22-4bc6-95b5-8f5dff43a0e5"), new Guid("c57b6454-603a-4597-8200-e09f8ca795fe") }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "role_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("80ae6925-a266-455c-9d0c-dc4cd3205ba4"), new Guid("43d12322-38c7-4f89-a95b-b941dfa9564c") },
                    { new Guid("80ae6925-a266-455c-9d0c-dc4cd3205ba4"), new Guid("97775ef6-6c52-42ff-8378-0321b5caef4f") },
                    { new Guid("80ae6925-a266-455c-9d0c-dc4cd3205ba4"), new Guid("b117f14c-2d22-4bc6-95b5-8f5dff43a0e5") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_email_confirmation_tokens_user_id",
                table: "email_confirmation_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_refesh_tokens_user_id",
                table: "refesh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_event_event_role_id",
                table: "user_event",
                column: "event_role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_user_id",
                table: "user_roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_create_by",
                table: "users",
                column: "create_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "email_confirmation_tokens");

            migrationBuilder.DropTable(
                name: "refesh_tokens");

            migrationBuilder.DropTable(
                name: "user_event");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "event_roles");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
