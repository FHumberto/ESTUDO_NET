using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Tier.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Created", "Name", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), "Tecnologia", new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), "Educação", new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), "Saúde", new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created", "Email", "FirstName", "LastName", "PasswordHash", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), "joao.silva@example.com", "João", "Silva", new byte[] { 32, 33 }, new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), "maria.oliveira@example.com", "Maria", "Oliveira", new byte[] { 32, 33 }, new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), "carlos.santos@example.com", "Carlos", "Santos", new byte[] { 32, 33 }, new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "Created", "Tittle", "Updated", "UserId" },
                values: new object[,]
                {
                    { 1, "Este é o corpo do primeiro post.", new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), "Primeiro Post", new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Este é o corpo do segundo post.", new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), "Segundo Post", new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Este é o corpo do terceiro post.", new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), "Terceiro Post", new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Body", "Created", "PostId", "Updated", "UserId" },
                values: new object[,]
                {
                    { 1, "Este é o primeiro comentário.", new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Este é o segundo comentário.", new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Este é o terceiro comentário.", new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
