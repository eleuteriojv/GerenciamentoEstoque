using GerenciamentoEstoque.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoEstoque.Api.Data.Seeds
{
    public static class PopulateSeeds
    {
        public static void PopulateDataBase(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Nome = "Lápis", Preco = 2M },
                new Produto { Id = 2, Nome = "Estojo", Preco = 4M },
                new Produto { Id = 3, Nome = "Caderno", Preco = 10M }
                );


            modelBuilder.Entity<Loja>().HasData(
                new Loja { Id = 1, Nome = "Papelaria", Endereco = "Rua 33, Vila Santa Cecília" }
                );

            modelBuilder.Entity<ItemEstoque>().HasData(
                new ItemEstoque { Quantidade = 10, LojaId = 1, ProdutoId = 1 },
                new ItemEstoque { Quantidade = 30, LojaId = 1, ProdutoId = 2 },
                new ItemEstoque { Quantidade = 20, LojaId = 1, ProdutoId = 3 }
                );

        }
    }
}
