using GerenciamentoEstoque.Api.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTest.ControllersApi
{
    public class ItemEstoqueControllerTest
    {

        public ItemEstoqueControllerTest()
        {
            var options = new DbContextOptionsBuilder<GerenciamentoDbContext>()
                .UseInMemoryDatabase(databaseName: "test_gerenciamento")
                .Options;

            var context = new GerenciamentoDbContext(options);
        }
        [Fact]
        public void DadoRecebimento_PeloIdDeUmItemNoEstoque()
        {
            var options = new DbContextOptionsBuilder<GerenciamentoDbContext>()
                .UseInMemoryDatabase(databaseName: "test_gerenciamento")
                .Options;

            var context = new GerenciamentoDbContext(options);
            int idProduto = 1;
            int idLoja = 1;

            var itemEstoque = context.ItemEstoques
                .Include(x => x.Produtos)
                .Include(x => x.Lojas)
                .FirstOrDefaultAsync(x => x.ProdutoId == idProduto && x.LojaId == idLoja);

            Assert.NotNull(itemEstoque);
        }
    }
}
