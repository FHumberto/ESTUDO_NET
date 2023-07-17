using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWiki_DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class BookDetailFixName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfCharpters",
                table: "BookDetails",
                newName: "NumberOfChapters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfChapters",
                table: "BookDetails",
                newName: "NumberOfCharpters");
        }
    }
}
