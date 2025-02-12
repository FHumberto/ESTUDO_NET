using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Tier.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixEntitySchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.EnsureSchema(
                name: "blog");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tags",
                newSchema: "blog");

            migrationBuilder.RenameTable(
                name: "PostTag",
                newName: "PostTag",
                newSchema: "blog");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Posts",
                newSchema: "blog");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comments",
                newSchema: "blog");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "Identity");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Tags",
                schema: "blog",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "PostTag",
                schema: "blog",
                newName: "PostTag");

            migrationBuilder.RenameTable(
                name: "Posts",
                schema: "blog",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "Comments",
                schema: "blog",
                newName: "Comments");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "Identity",
                newName: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7b1f8a4-8a66-4de0-9bf0-038cf34dec0a", "AQAAAAIAAYagAAAAEFtY7SEXXIyEZsciHj73Clnvil/GDXwobaJYn7aCxCXEhiD4aqDN5q96yXOvNFYB/w==", "10790d1c-8853-459f-ac80-b2e77c2cc7c8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7d96d38-45b1-4a3a-8a4d-746e4c929f64",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "982a001e-54e1-4145-a4ab-0cf0693ac9af", "AQAAAAIAAYagAAAAEJqXwgNabhv9aAKcR8j1KRLVe9NYgjpVkRqC2CFslxEIE1SIUNryavV3rZUzkgqHgA==", "dbd56d19-7fd2-42e9-849c-065b93a077c9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b85d4a97-5c55-469a-bc27-5477319a7864", "AQAAAAIAAYagAAAAEDjdPaYPOV0gk1fLR0pBdsw1BNr0P+Dh00+a9RgAOWMlWtX08B+qN43CAE+PzjQc9A==", "2aceda86-c0b9-490c-a5c4-bd618b86c5bc" });
        }
    }
}
