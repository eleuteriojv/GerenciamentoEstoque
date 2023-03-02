using GerenciamentoEstoque.Api.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTest.ControllersApi
{
    public class LojaControllerTest
    {

        public LojaControllerTest()
        {
            var options = new DbContextOptionsBuilder<GerenciamentoDbContext>()
                .UseInMemoryDatabase(databaseName: "test_gerenciamento")
                .Options;

            var context = new GerenciamentoDbContext(options);
        }
        [Fact]
        public void DadoRecebimento_PeloIdDeUmaLoja()
        {
            var options = new DbContextOptionsBuilder<GerenciamentoDbContext>()
                .UseInMemoryDatabase(databaseName: "test_gerenciamento")
                .Options;

            var context = new GerenciamentoDbContext(options);
            int id = 1;

            var loja = context.Lojas.FindAsync(id);

            Assert.NotNull(loja);
        }
    }
}

