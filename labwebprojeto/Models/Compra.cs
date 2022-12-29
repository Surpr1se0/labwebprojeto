using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace labwebprojeto.Models
{
    [Table("Compra")]
    public class Compra
    {
        [Key]
        [Column("Id_Compra")]
        [Required(ErrorMessage = "Id Compra is Required")]
        public int IdCompra { get; set; }

        [Column("Id_Jogo")]
        public int IdJogo { get; set; }

        [Column("Id_Utilizador")]
        public int IdUtilizador { get; set; }

        [Column("data_compra", TypeName = "datetime")]
        [DisplayName("Data de Compra")]
        public DateTime DataCompra { get; set; }

        [ForeignKey("IdJogo")]
        [InverseProperty("Compras")]
        public virtual Jogo IdJogoNavigation { get; set; } = null!;

        [ForeignKey("IdUtilizador")]
        [InverseProperty("Compras")]
        public virtual Utilizador IdUtilizadorNavigation { get; set; } = null!;
    }
}
