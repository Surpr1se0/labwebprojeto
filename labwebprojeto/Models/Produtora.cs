using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace labwebprojeto.Models
{
    [Table("Produtora")]
    public partial class Produtora
    {
        public Produtora()
        {
            Jogos = new HashSet<Jogo>();
        }

        [Key]
        [Column("Id_Produtora")]
        public int IdProdutora { get; set; }
        [Column("nome")]
        [StringLength(50)]
        public string? Nome { get; set; }

        [InverseProperty("IdProdutoraNavigation")]
        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
