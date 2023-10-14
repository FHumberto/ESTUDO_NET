using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations;

/// <inheritdoc />
public partial class ProdutosSetoresMap : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "IdSetor",
            table: "Produtos",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<string>(
            name: "Un",
            table: "Produtos",
            type: "varchar(3)",
            maxLength: 3,
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateTable(
            name: "Setores",
            columns: table => new
            {
                IdSetor = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Setores", x => x.IdSetor);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Setores");

        migrationBuilder.DropColumn(
            name: "IdSetor",
            table: "Produtos");

        migrationBuilder.DropColumn(
            name: "Un",
            table: "Produtos");
    }
}
