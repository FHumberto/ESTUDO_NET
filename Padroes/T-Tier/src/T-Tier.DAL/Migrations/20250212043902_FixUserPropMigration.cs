using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Tier.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixUserPropMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "Identity",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "Identity",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

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
    }
}
