using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using labwebprojeto.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace labwebprojeto.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<Consola> Consolas { get; set; } = null!;
        public virtual DbSet<Favorito> Favoritos { get; set; } = null!;
        public virtual DbSet<Jogo> Jogos { get; set; } = null!;
        public virtual DbSet<Produtora> Produtoras { get; set; } = null!;
        public virtual DbSet<Utilizador> Utilizadors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__CB90334950410431");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK__Compra__661E0ED0060EB70A");

                entity.HasOne(d => d.IdJogoNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdJogo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Compra__Id_Jogo__693CA210");

                entity.HasOne(d => d.IdUtilizadorNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdUtilizador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Compra__Id_Utili__6A30C649");
            });

            modelBuilder.Entity<Consola>(entity =>
            {
                entity.HasKey(e => e.IdConsola)
                    .HasName("PK__Consola__EF167BDDD47BBD8A");
            });

            modelBuilder.Entity<Favorito>(entity =>
            {
                entity.HasKey(e => e.IdFavorito)
                    .HasName("PK__Favorito__6DACC00DC83C35E0");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Favoritos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorito__Id_Cat__6D0D32F4");

                entity.HasOne(d => d.IdUtilizadorNavigation)
                    .WithMany(p => p.Favoritos)
                    .HasForeignKey(d => d.IdUtilizador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorito__Id_Uti__6E01572D");
            });

            modelBuilder.Entity<Jogo>(entity =>
            {
                entity.HasKey(e => e.IdJogos)
                    .HasName("PK__Jogo__7B4D1FE19E5B4DB9");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Jogos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jogo__Id_Categor__6477ECF3");

                entity.HasOne(d => d.IdConsolaNavigation)
                    .WithMany(p => p.Jogos)
                    .HasForeignKey(d => d.IdConsola)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jogo__Id_Consola__656C112C");

                entity.HasOne(d => d.IdProdutoraNavigation)
                    .WithMany(p => p.Jogos)
                    .HasForeignKey(d => d.IdProdutora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jogo__Id_Produto__66603565");
            });

            modelBuilder.Entity<Produtora>(entity =>
            {
                entity.HasKey(e => e.IdProdutora)
                    .HasName("PK__Produtor__4FC8329532DCF4E4");
            });

            modelBuilder.Entity<Utilizador>(entity =>
            {
                entity.HasKey(e => e.IdUtilizador)
                    .HasName("PK__Utilizad__FEC354F1E2DADB37");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
