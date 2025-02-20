using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Tier.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixTagMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82b4f2b8-7d21-4668-b5df-5e9610390714", "AQAAAAIAAYagAAAAEAdQalcieZfdK/uMzQn70Zz78YX6jsXjElkCs7Rpp1AJ5mZrwxL59kP8e/CJiOFumg==", "b744788a-75fa-4267-a88e-c0727133478f" });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7d96d38-45b1-4a3a-8a4d-746e4c929f64",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51b319dd-c027-4756-a765-68986a0c0ef6", "AQAAAAIAAYagAAAAENVX3R49HE4cTkrFxIyldAWOv+in9GTdDGv3w6VgaZqrNlG8pQ0uJ/Md8DKbo0EZTw==", "f1468a2d-5450-4518-a833-ddf2e8e38c3a" });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a75633d-cc10-49ef-b53a-1932a9e229a8", "AQAAAAIAAYagAAAAEEBgohFgOjqe+TxMl+LGwKPwZjAFkvLGOdKToXXlmaCXPKL0DEH7XgHfURRaUXD0gA==", "cdcc4a8b-fc7a-49a0-b7d0-f8d9f2905edd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b972585-7bde-4a9a-beb9-2691ef1047bc", "AQAAAAIAAYagAAAAEA6yPqD0nJz5V05qLrYymceytMJPln3nkolAEET7KAJHZJFmgZR+IM0wHDwVIACXNA==", "6d331702-24ff-41db-a877-3d5699d4ae0b" });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7d96d38-45b1-4a3a-8a4d-746e4c929f64",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "208b1bc7-9a8f-43e8-8ffc-c8851801f519", "AQAAAAIAAYagAAAAEKVF3vtcF2fE8vpomT8cmpvJtIkpYrW6MuH0f9hIRKMazmBkXrqYzmehBbMFz6aeug==", "99ec07d8-0ec0-4f06-b099-754ad83611ae" });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b53966b-331b-4bbd-9056-a6e7e432cffc", "AQAAAAIAAYagAAAAENP9oHYll8BJ3QFFNqIRhjCAIxhGeOTLW3w32i8w9+RE84fh/9lt1n0s+BfPXT5eBQ==", "e84549f5-d59a-4283-96da-31325978978f" });
        }
    }
}
