using GerenciamentoEstoque.Api.Data.Mapeamento;
using GerenciamentoEstoque.Api.Data.Seeds;
using GerenciamentoEstoque.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoEstoque.Api.Data
{
    public class GerenciamentoDbContext : DbContext
    {
        public GerenciamentoDbContext(DbContextOptions<GerenciamentoDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ItemEstoque> ItemEstoques { get; set; }
        public DbSet<Loja> Lojas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.PopulateDataBase();
            modelBuilder.MapeamentoItemEstoque();
        }
    }

}
