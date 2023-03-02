using GerenciamentoEstoque.Api.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTest.ControllersApi
{
    public class ProdutoControllerTest
    {

        public ProdutoControllerTest()
        {
            var options = new DbContextOptionsBuilder<GerenciamentoDbContext>()
                .UseInMemoryDatabase(databaseName: "test_gerenciamento")
                .Options;

            var context = new GerenciamentoDbContext(options);
        }
        [Fact]
        public void DadoRecebimento_PeloIdDeUmProduto()
        {
            var options = new DbContextOptionsBuilder<GerenciamentoDbContext>()
                .UseInMemoryDatabase(databaseName: "test_gerenciamento")
                .Options;

            var context = new GerenciamentoDbContext(options);
            int id = 1;

            var produto = context.Produtos.FindAsync(id);

            Assert.NotNull(produto);
        }
    }
}
