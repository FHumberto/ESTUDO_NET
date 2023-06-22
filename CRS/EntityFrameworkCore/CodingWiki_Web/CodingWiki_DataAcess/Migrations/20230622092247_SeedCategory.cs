﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWiki_DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories VALUES ('Cat 1')");
            migrationBuilder.Sql("INSERT INTO Categories VALUES ('Cat 2')");
            migrationBuilder.Sql("INSERT INTO Categories VALUES ('Cat 3')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
