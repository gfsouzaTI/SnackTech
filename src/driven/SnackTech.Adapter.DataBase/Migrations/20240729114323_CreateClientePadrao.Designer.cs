﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SnackTech.Adapter.DataBase.Context;
using SnackTech.Domain.Models;

#nullable disable

namespace SnackTech.Adapter.DataBase.Migrations
{
    [DbContext(typeof(RepositoryDbContext))]
    [Migration("20240729114323_CreateClientePadrao")]
    partial class CreateClientePadrao
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SnackTech.Domain.Models.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<Guid>("IdCliente")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("smallmoney");

                    b.HasKey("Id");

                    b.HasIndex("IdCliente");

                    b.ToTable("Pedido", (string)null);
                });

            modelBuilder.Entity("SnackTech.Domain.Models.PedidoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdPedido")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdProduto")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("Sequencial")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("smallmoney");

                    b.HasKey("Id");

                    b.HasIndex("IdPedido");

                    b.HasIndex("IdProduto");

                    b.ToTable("PedidoItem", (string)null);
                });

            modelBuilder.Entity("SnackTech.Domain.Models.Pessoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("Pessoa", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SnackTech.Domain.Models.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Categoria")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<decimal>("Valor")
                        .HasColumnType("smallmoney");

                    b.HasKey("Id");

                    b.ToTable("Produto", (string)null);
                });

            modelBuilder.Entity("SnackTech.Domain.Models.Cliente", b =>
                {
                    b.HasBaseType("SnackTech.Domain.Models.Pessoa");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.ToTable("Cliente", (string)null);

                    b.HasData(
                        new
                        {
                            Id = Guid.Parse("6ee54a46-007f-4e4c-9fe8-1a13eadf7fd1"),
                            Nome = "Cliente Padrão",
                            Cpf = Cliente.CPF_CLIENTE_PADRAO,
                            Email = "cliente.padrao@padrao.com"
                        });
                });

            modelBuilder.Entity("SnackTech.Domain.Models.Pedido", b =>
                {
                    b.HasOne("SnackTech.Domain.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("SnackTech.Domain.Models.PedidoItem", b =>
                {
                    b.HasOne("SnackTech.Domain.Models.Pedido", "Pedido")
                        .WithMany("Itens")
                        .HasForeignKey("IdPedido")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SnackTech.Domain.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("IdProduto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("SnackTech.Domain.Models.Cliente", b =>
                {
                    b.HasOne("SnackTech.Domain.Models.Pessoa", null)
                        .WithOne()
                        .HasForeignKey("SnackTech.Domain.Models.Cliente", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SnackTech.Domain.Models.Pedido", b =>
                {
                    b.Navigation("Itens");
                });
#pragma warning restore 612, 618
        }
    }
}
