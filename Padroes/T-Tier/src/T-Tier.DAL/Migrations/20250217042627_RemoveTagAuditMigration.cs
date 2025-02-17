using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Tier.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTagAuditMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "blog",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "blog",
                table: "Tags");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "blog",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "blog",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5f8d05f-0757-4938-89b9-cd13501dd7db", "AQAAAAIAAYagAAAAEMyOLbQyPBGVp4W6JWAXddxUKdFILkPHy31Gc7f1ap0fmN1jeBRFRqh+HZQUVAi+GQ==", "b65f20f0-78a0-4c3a-a81a-e88d09de1b27" });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7d96d38-45b1-4a3a-8a4d-746e4c929f64",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8d057df-8c19-4a31-ae22-d7e53f49b701", "AQAAAAIAAYagAAAAEIeVFwJi0cMmkw5L+QX9HfNlzwI5Lzx/YXy6Ab3E6kMAVqgs5il7dquu/CMMOwpXZw==", "7ad99721-76e2-44a4-b9ae-97211b27e487" });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "456c8374-f1fd-4c50-847a-f53164c90dd1", "AQAAAAIAAYagAAAAEPQUluQb4q+fxuuFSGlMfZEitwtzfGh5lPE1iEE+lD9iFrZeVax6KZcCypY47z4JVg==", "37d0bf1a-62e1-4819-b524-25ad0dec7ccd" });

            migrationBuilder.UpdateData(
                schema: "blog",
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "UpdatedBy" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "blog",
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedBy", "UpdatedBy" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "blog",
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedBy", "UpdatedBy" },
                values: new object[] { null, null });
        }
    }
}
