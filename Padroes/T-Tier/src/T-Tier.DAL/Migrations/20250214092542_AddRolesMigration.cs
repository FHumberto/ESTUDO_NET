using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Tier.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "Default", "DEFAULT" }
                });

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

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5" },
                    { "2", "c7d96d38-45b1-4a3a-8a4d-746e4c929f64" },
                    { "1", "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "c7d96d38-45b1-4a3a-8a4d-746e4c929f64" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9" });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2eeb4f98-2ae0-4991-a21b-425852ef4ba6", "AQAAAAIAAYagAAAAEMVo5HP1Ie5sSddwkvEuRfiui+BopJiaCMU6TnuTkfP840etJv9w+UxsU8dPJeHSrQ==", "e8a86cbe-30aa-4462-a176-9ab03551b86c" });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7d96d38-45b1-4a3a-8a4d-746e4c929f64",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b825d8de-5a45-4751-92be-cc0bed1f1195", "AQAAAAIAAYagAAAAEDNJ5LjZwO7/vsFxTCKInBZVwiKFuVf/fAoFy7Ap5CzJXlm1k0aQUr8pK8iMspyT5Q==", "bf6dc393-a0c8-44df-99b5-08965fd42742" });

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ce8e01c-23ef-4590-ba6d-a89ea71b5eef", "AQAAAAIAAYagAAAAELYaM1CjkXyRyCP1JrrxOkyZPRxytb97isJG/vzivHagO3EtMZEGSySUVCFooyxFDA==", "90cf7ae6-469c-4029-8e76-b73b71adc923" });
        }
    }
}
