using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Tier.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixIdentitySchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "Identity");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60e86d59-310c-4eba-ae20-949995eddaf5", "AQAAAAIAAYagAAAAENO5O8TLH5PTJBscfslMqcOm0OIDEtQQQwkpd1USqU4DpamonINjzWmUaYv+Q4I49w==", "ddbaaca6-a545-4da0-bb8e-5f4ba13512a2" });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7d96d38-45b1-4a3a-8a4d-746e4c929f64",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b570b6d-dbdf-4424-a0ec-1e80768b8679", "AQAAAAIAAYagAAAAEJDG31/YCCSqJ4HEIDtAmanngryBnqWixYR77rICl6E1mls+XW+umVJ/nOBvPdL24Q==", "f83a5503-0334-46b9-b850-b15be21638a0" });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f39bcf3f-3521-41b6-975b-f17be49eb1fe", "AQAAAAIAAYagAAAAEFQd089Y7RHF9289MqtRrIbzFEWQnZh+eAVr0e/OIgdX8TExRpyCKHLsKlNYSR1Dqg==", "46b2d07f-7960-4435-b970-fab925cae234" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "Identity",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "Identity",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "Identity",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "Identity",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "Identity",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "Identity",
                newName: "AspNetRoleClaims");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5e04e20-10d6-4c44-a1fe-cdbb88238a0f", "AQAAAAIAAYagAAAAEB6d4JPMLjPGEudxIJwSNcbi7UcR/A/4nZYuiYFArp7Rng1QPRnrj25YpxTBEpX5Aw==", "5cb7f84d-1541-47c4-8b63-d1dd38a60987" });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7d96d38-45b1-4a3a-8a4d-746e4c929f64",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9dcc64f4-6a1f-42a6-b555-165aafc31b9a", "AQAAAAIAAYagAAAAEHfXj99PLkjRGLsKeDmxuAQK3NonwpK7aKnQ+SWOHAggLvJhFH8SVEgSxv8npqLAyA==", "09c8ddf5-ceb9-456c-b00c-4d9f9fc1875d" });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fab9caf2-6c42-4fe1-829d-e7908993d63a", "AQAAAAIAAYagAAAAEA94g8Nv/oMIb/V/kXoUdnKT44tHXKkaxoRyK/o+Js6j/F2iRdpsVMoKCqHqiV8KVg==", "8b550b8b-eccf-4307-95fb-c02de7432a4a" });
        }
    }
}
