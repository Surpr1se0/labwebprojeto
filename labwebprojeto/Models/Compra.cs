using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace labwebprojeto.Models
{
    [Table("Compra")]
    public partial class Compra
    {
        [Key]
        [Column("Id_Compra")]
        public int IdCompra { get; set; }
        [Column("Id_Jogo")]
        public int IdJogo { get; set; }
        [Column("Id_Utilizador")]
        public int IdUtilizador { get; set; }
        [Column("data_compra", TypeName = "datetime")]
        public DateTime DataCompra { get; set; }

        [ForeignKey("IdJogo")]
        [InverseProperty("Compras")]
        public virtual Jogo IdJogoNavigation { get; set; } = null!;
        [ForeignKey("IdUtilizador")]
        [InverseProperty("Compras")]
        public virtual Utilizador IdUtilizadorNavigation { get; set; } = null!;
    }
}
