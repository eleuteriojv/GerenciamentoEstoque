using Microsoft.EntityFrameworkCore.Migrations;

namespace GerenciamentoEstoque.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemEstoques",
                columns: table => new
                {
                    LojaId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemEstoques", x => new { x.ProdutoId, x.LojaId });
                    table.ForeignKey(
                        name: "FK_ItemEstoques_Lojas_LojaId",
                        column: x => x.LojaId,
                        principalTable: "Lojas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemEstoques_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Lojas",
                columns: new[] { "Id", "Endereco", "Nome" },
                values: new object[] { 1, "Rua 33, Vila Santa Cecília", "Papelaria" });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Nome", "Preco" },
                values: new object[,]
                {
                    { 1, "Lápis", 2m },
                    { 2, "Estojo", 4m },
                    { 3, "Caderno", 10m }
                });

            migrationBuilder.InsertData(
                table: "ItemEstoques",
                columns: new[] { "LojaId", "ProdutoId", "Quantidade" },
                values: new object[] { 1, 1, 10 });

            migrationBuilder.InsertData(
                table: "ItemEstoques",
                columns: new[] { "LojaId", "ProdutoId", "Quantidade" },
                values: new object[] { 1, 2, 30 });

            migrationBuilder.InsertData(
                table: "ItemEstoques",
                columns: new[] { "LojaId", "ProdutoId", "Quantidade" },
                values: new object[] { 1, 3, 20 });

            migrationBuilder.CreateIndex(
                name: "IX_ItemEstoques_LojaId",
                table: "ItemEstoques",
                column: "LojaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemEstoques");

            migrationBuilder.DropTable(
                name: "Lojas");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
