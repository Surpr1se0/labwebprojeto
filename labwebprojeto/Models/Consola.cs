using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace labwebprojeto.Models
{
    [Table("Consola")]
    public partial class Consola
    {
        public Consola()
        {
            Jogos = new HashSet<Jogo>();
        }

        [Key]
        [Column("Id_Consola")]
        public int IdConsola { get; set; }
        [Column("nome")]
        [StringLength(50)]
        public string Nome { get; set; } = null!;

        [InverseProperty("IdConsolaNavigation")]
        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
