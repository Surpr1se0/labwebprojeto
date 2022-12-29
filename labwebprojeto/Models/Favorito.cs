using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace labwebprojeto.Models
{
    [Table("Favorito")]
    public class Favorito
    {
        [Key]
        [Column("Id_Favorito")]
        public int IdFavorito { get; set; }
        [Column("Id_Categoria")]
        public int IdCategoria { get; set; }
        [Column("Id_Utilizador")]
        public int IdUtilizador { get; set; }

        [ForeignKey("IdCategoria")]
        [InverseProperty("Favoritos")]
        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

        [ForeignKey("IdUtilizador")]
        [InverseProperty("Favoritos")]
        public virtual Utilizador IdUtilizadorNavigation { get; set; } = null!;
    }
}
