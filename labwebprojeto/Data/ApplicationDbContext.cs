using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using labwebprojeto.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

namespace labwebprojeto.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<Consola> Consolas { get; set; }
        public virtual DbSet<Favorito> Favoritos { get; set; }
        public virtual DbSet<Jogo> Jogos { get; set; } = null!;
        public virtual DbSet<Produtora> Produtoras { get; set; }
        public virtual DbSet<Utilizador> Utilizadors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _SeedCategoria(modelBuilder);
            _SeedConsola(modelBuilder);
            _SeedProdutora(modelBuilder);
            _SeedJogo(modelBuilder);

            base.OnModelCreating(modelBuilder);

            _configureCategoria(modelBuilder);
            _configureUtilizador(modelBuilder);
            _configureCompras(modelBuilder);
            _configureConsola(modelBuilder);
            _configureFavorito(modelBuilder);
            _configureJogo(modelBuilder);
            _configureProdutora(modelBuilder);
            _configureUtilizador(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        //*DATA_SEEDING*//
        private void _SeedUsers(ModelBuilder modelBuilder)
        {
            /*SYS-ADMIN*/
            IdentityUser user = new IdentityUser()
            {
                UserName = "SysAdmin",
                NormalizedUserName = "SYSADMIN",
                Email = "sysadmin@gmail.com",
                NormalizedEmail = "SYSADMIN@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "SYSAdmin*123");
            modelBuilder.Entity<IdentityUser>().HasData(user);
        }

        private void _SeedCategoria(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData
            (
                new Categoria
                {
                    IdCategoria = 1,
                    Nome = "FPS"
                },
                new Categoria
                {
                    IdCategoria = 2,
                    Nome = "Sports"
                },
                new Categoria
                {
                    IdCategoria = 3,
                    Nome = "Action"
                }
            );
        }

        private void _SeedConsola(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consola>().HasData
            (
                new Consola
                {
                    IdConsola = 1,
                    Nome = "PS5"
                },
                new Consola
                {
                    IdConsola = 2,
                    Nome = "XBox Series X"
                },
                new Consola
                {
                    IdConsola = 3,
                    Nome = "PC"
                }
            );
        }

        private void _SeedProdutora(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produtora>().HasData
            (
                new Produtora
                {
                    IdProdutora = 1,
                    Nome = "EA Games"
                },
                new Produtora
                {
                    IdProdutora = 2,
                    Nome = "Rockstar Games"
                },
                new Produtora
                {
                    IdProdutora = 3,
                    Nome = "Blizzard"
                }
            );
        }

        private void _SeedJogo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogo>().HasData
            (
                new Jogo
                {
                    IdJogos = 1,
                    Nome = "The Witcher",
                    Foto = "https://res.cloudinary.com/dghxejpvl/image/upload/v1673112685/qomoenwxorzr3gkssezm.jpg",
                    Foto1 = "https://res.cloudinary.com/dghxejpvl/image/upload/v1673112684/yttpg2vaa2bwkpjysibe.jpg",
                    Foto2 = "https://res.cloudinary.com/dghxejpvl/image/upload/v1673112686/wy8lyivtkxskxapvkk4p.png",
                    IdCategoria = 3,
                    IdConsola = 3,
                    IdProdutora = 3,
                    Descricao = "Jogo de Fantasia",
                    Descricao1 = "Game Of The Year 2019"
                }
            );
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        //*CONFIGURES*//

        #region helpers
        private void _configureCategoria(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__CB90334950410431");
            });
        }

        private void _configureUtilizador(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utilizador>(entity =>
            {
                entity.HasKey(e => e.IdUtilizador)
                    .HasName("PK__Utilizad__FEC354F1E2DADB37");
            });
        }

        private void _configureProdutora(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produtora>(entity =>
            {
                entity.HasKey(e => e.IdProdutora)
                    .HasName("PK__Produtor__4FC8329532DCF4E4");
            });
        }

        private void _configureJogo(ModelBuilder modelBuilder)
        {
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
        }

        private void _configureFavorito(ModelBuilder modelBuilder)
        {
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
        }

        private void _configureConsola(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consola>(entity =>
            {
                entity.HasKey(e => e.IdConsola)
                    .HasName("PK__Consola__EF167BDDD47BBD8A");
            });
        }

        private void _configureCompras(ModelBuilder modelBuilder)
        {
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
        }

        #endregion


    }
}
