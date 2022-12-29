using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace labwebprojeto.Models
{
    public partial class Categorium
    {
        public Categorium()
        {
            Favoritos = new HashSet<Favorito>();
            Jogos = new HashSet<Jogo>();
        }

        [Key]
        [Column("Id_Categoria")]
        public int IdCategoria { get; set; }
        [Column("nome")]
        [StringLength(50)]
        public string Nome { get; set; } = null!;

        [InverseProperty("IdCategoriaNavigation")]
        public virtual ICollection<Favorito> Favoritos { get; set; }
        [InverseProperty("IdCategoriaNavigation")]
        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
