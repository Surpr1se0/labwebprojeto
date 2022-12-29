﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using labwebprojeto.Data;

#nullable disable

namespace labwebprojeto.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221229172846_SecondStep")]
    partial class SecondStep
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("labwebprojeto.Models.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_Categoria");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategoria"), 1L, 1);

                    b.Property<int?>("JogoIdJogos")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nome");

                    b.HasKey("IdCategoria")
                        .HasName("PK__Categori__CB90334950410431");

                    b.HasIndex("JogoIdJogos");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("labwebprojeto.Models.Compra", b =>
                {
                    b.Property<int>("IdCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_Compra");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCompra"), 1L, 1);

                    b.Property<DateTime>("DataCompra")
                        .HasColumnType("datetime")
                        .HasColumnName("data_compra");

                    b.Property<int>("IdJogo")
                        .HasColumnType("int")
                        .HasColumnName("Id_Jogo");

                    b.Property<int>("IdUtilizador")
                        .HasColumnType("int")
                        .HasColumnName("Id_Utilizador");

                    b.HasKey("IdCompra")
                        .HasName("PK__Compra__661E0ED0060EB70A");

                    b.HasIndex("IdJogo");

                    b.HasIndex("IdUtilizador");

                    b.ToTable("Compra");
                });

            modelBuilder.Entity("labwebprojeto.Models.Consola", b =>
                {
                    b.Property<int>("IdConsola")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_Consola");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdConsola"), 1L, 1);

                    b.Property<int?>("JogoIdJogos")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nome");

                    b.HasKey("IdConsola")
                        .HasName("PK__Consola__EF167BDDD47BBD8A");

                    b.HasIndex("JogoIdJogos");

                    b.ToTable("Consola");
                });

            modelBuilder.Entity("labwebprojeto.Models.Favorito", b =>
                {
                    b.Property<int>("IdFavorito")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_Favorito");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFavorito"), 1L, 1);

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int")
                        .HasColumnName("Id_Categoria");

                    b.Property<int>("IdUtilizador")
                        .HasColumnType("int")
                        .HasColumnName("Id_Utilizador");

                    b.HasKey("IdFavorito")
                        .HasName("PK__Favorito__6DACC00DC83C35E0");

                    b.HasIndex("IdCategoria");

                    b.HasIndex("IdUtilizador");

                    b.ToTable("Favorito");
                });

            modelBuilder.Entity("labwebprojeto.Models.Jogo", b =>
                {
                    b.Property<int>("IdJogos")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_Jogos");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdJogos"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("descricao");

                    b.Property<string>("Descricao1")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("descricao1");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("foto");

                    b.Property<string>("Foto1")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("foto1");

                    b.Property<string>("Foto2")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("foto_2");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int")
                        .HasColumnName("Id_Categoria");

                    b.Property<int>("IdConsola")
                        .HasColumnType("int")
                        .HasColumnName("Id_Consola");

                    b.Property<int>("IdProdutora")
                        .HasColumnType("int")
                        .HasColumnName("Id_Produtora");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasColumnName("nome");

                    b.Property<decimal>("Preco")
                        .HasColumnType("money")
                        .HasColumnName("preco");

                    b.HasKey("IdJogos")
                        .HasName("PK__Jogo__7B4D1FE19E5B4DB9");

                    b.HasIndex("IdCategoria");

                    b.HasIndex("IdConsola");

                    b.HasIndex("IdProdutora");

                    b.ToTable("Jogo");
                });

            modelBuilder.Entity("labwebprojeto.Models.Produtora", b =>
                {
                    b.Property<int>("IdProdutora")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_Produtora");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProdutora"), 1L, 1);

                    b.Property<int?>("JogoIdJogos")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nome");

                    b.HasKey("IdProdutora")
                        .HasName("PK__Produtor__4FC8329532DCF4E4");

                    b.HasIndex("JogoIdJogos");

                    b.ToTable("Produtora");
                });

            modelBuilder.Entity("labwebprojeto.Models.Utilizador", b =>
                {
                    b.Property<int>("IdUtilizador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_Utilizador");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUtilizador"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nome");

                    b.Property<string>("Telefone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("telefone");

                    b.HasKey("IdUtilizador")
                        .HasName("PK__Utilizad__FEC354F1E2DADB37");

                    b.ToTable("Utilizador");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("labwebprojeto.Models.Categoria", b =>
                {
                    b.HasOne("labwebprojeto.Models.Jogo", null)
                        .WithMany("Categorias")
                        .HasForeignKey("JogoIdJogos");
                });

            modelBuilder.Entity("labwebprojeto.Models.Compra", b =>
                {
                    b.HasOne("labwebprojeto.Models.Jogo", "IdJogoNavigation")
                        .WithMany("Compras")
                        .HasForeignKey("IdJogo")
                        .IsRequired()
                        .HasConstraintName("FK__Compra__Id_Jogo__693CA210");

                    b.HasOne("labwebprojeto.Models.Utilizador", "IdUtilizadorNavigation")
                        .WithMany("Compras")
                        .HasForeignKey("IdUtilizador")
                        .IsRequired()
                        .HasConstraintName("FK__Compra__Id_Utili__6A30C649");

                    b.Navigation("IdJogoNavigation");

                    b.Navigation("IdUtilizadorNavigation");
                });

            modelBuilder.Entity("labwebprojeto.Models.Consola", b =>
                {
                    b.HasOne("labwebprojeto.Models.Jogo", null)
                        .WithMany("Consolas")
                        .HasForeignKey("JogoIdJogos");
                });

            modelBuilder.Entity("labwebprojeto.Models.Favorito", b =>
                {
                    b.HasOne("labwebprojeto.Models.Categoria", "IdCategoriaNavigation")
                        .WithMany("Favoritos")
                        .HasForeignKey("IdCategoria")
                        .IsRequired()
                        .HasConstraintName("FK__Favorito__Id_Cat__6D0D32F4");

                    b.HasOne("labwebprojeto.Models.Utilizador", "IdUtilizadorNavigation")
                        .WithMany("Favoritos")
                        .HasForeignKey("IdUtilizador")
                        .IsRequired()
                        .HasConstraintName("FK__Favorito__Id_Uti__6E01572D");

                    b.Navigation("IdCategoriaNavigation");

                    b.Navigation("IdUtilizadorNavigation");
                });

            modelBuilder.Entity("labwebprojeto.Models.Jogo", b =>
                {
                    b.HasOne("labwebprojeto.Models.Categoria", "IdCategoriaNavigation")
                        .WithMany("Jogos")
                        .HasForeignKey("IdCategoria")
                        .IsRequired()
                        .HasConstraintName("FK__Jogo__Id_Categor__6477ECF3");

                    b.HasOne("labwebprojeto.Models.Consola", "IdConsolaNavigation")
                        .WithMany("Jogos")
                        .HasForeignKey("IdConsola")
                        .IsRequired()
                        .HasConstraintName("FK__Jogo__Id_Consola__656C112C");

                    b.HasOne("labwebprojeto.Models.Produtora", "IdProdutoraNavigation")
                        .WithMany("Jogos")
                        .HasForeignKey("IdProdutora")
                        .IsRequired()
                        .HasConstraintName("FK__Jogo__Id_Produto__66603565");

                    b.Navigation("IdCategoriaNavigation");

                    b.Navigation("IdConsolaNavigation");

                    b.Navigation("IdProdutoraNavigation");
                });

            modelBuilder.Entity("labwebprojeto.Models.Produtora", b =>
                {
                    b.HasOne("labwebprojeto.Models.Jogo", null)
                        .WithMany("Produtoras")
                        .HasForeignKey("JogoIdJogos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("labwebprojeto.Models.Categoria", b =>
                {
                    b.Navigation("Favoritos");

                    b.Navigation("Jogos");
                });

            modelBuilder.Entity("labwebprojeto.Models.Consola", b =>
                {
                    b.Navigation("Jogos");
                });

            modelBuilder.Entity("labwebprojeto.Models.Jogo", b =>
                {
                    b.Navigation("Categorias");

                    b.Navigation("Compras");

                    b.Navigation("Consolas");

                    b.Navigation("Produtoras");
                });

            modelBuilder.Entity("labwebprojeto.Models.Produtora", b =>
                {
                    b.Navigation("Jogos");
                });

            modelBuilder.Entity("labwebprojeto.Models.Utilizador", b =>
                {
                    b.Navigation("Compras");

                    b.Navigation("Favoritos");
                });
#pragma warning restore 612, 618
        }
    }
}
