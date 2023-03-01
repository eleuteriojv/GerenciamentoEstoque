﻿// <auto-generated />
using GerenciamentoEstoque.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GerenciamentoEstoque.Api.Migrations
{
    [DbContext(typeof(GerenciamentoDbContext))]
    [Migration("20230301192223_AtualizandoBanco")]
    partial class AtualizandoBanco
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GerenciamentoEstoque.Api.Models.ItemEstoque", b =>
                {
                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("LojaId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("ProdutoId", "LojaId");

                    b.HasIndex("LojaId");

                    b.ToTable("ItemEstoques");

                    b.HasData(
                        new
                        {
                            ProdutoId = 1,
                            LojaId = 1,
                            Quantidade = 10
                        },
                        new
                        {
                            ProdutoId = 2,
                            LojaId = 1,
                            Quantidade = 30
                        },
                        new
                        {
                            ProdutoId = 3,
                            LojaId = 1,
                            Quantidade = 20
                        });
                });

            modelBuilder.Entity("GerenciamentoEstoque.Api.Models.Loja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lojas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Endereco = "Rua 33, Vila Santa Cecília",
                            Nome = "Papelaria"
                        });
                });

            modelBuilder.Entity("GerenciamentoEstoque.Api.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Produtos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Lápis",
                            Preco = 2m
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Estojo",
                            Preco = 4m
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Caderno",
                            Preco = 10m
                        });
                });

            modelBuilder.Entity("GerenciamentoEstoque.Api.Models.ItemEstoque", b =>
                {
                    b.HasOne("GerenciamentoEstoque.Api.Models.Loja", "Lojas")
                        .WithMany("Estoques")
                        .HasForeignKey("LojaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GerenciamentoEstoque.Api.Models.Produto", "Produtos")
                        .WithMany("Estoques")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lojas");

                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("GerenciamentoEstoque.Api.Models.Loja", b =>
                {
                    b.Navigation("Estoques");
                });

            modelBuilder.Entity("GerenciamentoEstoque.Api.Models.Produto", b =>
                {
                    b.Navigation("Estoques");
                });
#pragma warning restore 612, 618
        }
    }
}
