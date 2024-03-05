using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "LeaveTypes",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "LeaveRequests",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "LeaveAllocations",
                newName: "CreatedBy");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 3, 5, 5, 10, 46, 428, DateTimeKind.Local).AddTicks(7781), new DateTime(2024, 3, 5, 5, 10, 46, 428, DateTimeKind.Local).AddTicks(7795) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "LeaveTypes",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "LeaveRequests",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "LeaveAllocations",
                newName: "Created");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 3, 5, 5, 1, 16, 683, DateTimeKind.Local).AddTicks(3960), new DateTime(2024, 3, 5, 5, 1, 16, 683, DateTimeKind.Local).AddTicks(3973) });
        }
    }
}
