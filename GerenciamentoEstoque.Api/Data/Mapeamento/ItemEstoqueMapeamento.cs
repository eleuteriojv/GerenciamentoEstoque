using GerenciamentoEstoque.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoEstoque.Api.Data.Mapeamento
{
    public static class ItemEstoqueMapeamento
    {
        public static void MapeamentoItemEstoque(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemEstoque>()
                .HasKey(pe => new { pe.ProdutoId, pe.LojaId });
            modelBuilder.Entity<ItemEstoque>()
                .HasOne(pe => pe.Lojas)
                .WithMany(pe => pe.Estoques)
                .HasForeignKey(pe => pe.LojaId);

            modelBuilder.Entity<ItemEstoque>()
                .HasOne(pe => pe.Produtos)
                .WithMany(pe => pe.Estoques)
                .HasForeignKey(pe => pe.ProdutoId);

        }
    }
}
