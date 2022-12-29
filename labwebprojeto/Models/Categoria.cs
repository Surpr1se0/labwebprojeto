using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace labwebprojeto.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        public Categoria()
        {
            Favoritos = new HashSet<Favorito>();
            Jogos = new HashSet<Jogo>();
        }

        [Key]
        [Column("Id_Categoria")]
        public int IdCategoria { get; set; }

        [Column("nome")]
        [StringLength(50)]
        [DisplayName("Categoria")]
        public string Nome { get; set; } = null!;

        [InverseProperty("IdCategoriaNavigation")]
        public virtual ICollection<Favorito> Favoritos { get; set; }
        [InverseProperty("IdCategoriaNavigation")]
        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
