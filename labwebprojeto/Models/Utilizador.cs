using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace labwebprojeto.Models
{
    [Table("Utilizador")]
    public partial class Utilizador
    {
        public Utilizador()
        {
            Compras = new HashSet<Compra>();
            Favoritos = new HashSet<Favorito>();
        }

        [Key]
        [Column("Id_Utilizador")]
        public int IdUtilizador { get; set; }
        [Column("nome")]
        [StringLength(50)]
        public string Nome { get; set; } = null!;
        [Column("telefone")]
        [StringLength(20)]
        public string? Telefone { get; set; }

        [InverseProperty("IdUtilizadorNavigation")]
        public virtual ICollection<Compra> Compras { get; set; }
        [InverseProperty("IdUtilizadorNavigation")]
        public virtual ICollection<Favorito> Favoritos { get; set; }
    }
}
