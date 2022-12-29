using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace labwebprojeto.Models
{
    [Table("Utilizador")]
    public class Utilizador
    {
        public Utilizador()
        {
            Compras = new HashSet<Compra>();
            Favoritos = new HashSet<Favorito>();
        }

        [Key]
        [Column("Id_Utilizador")]
        [Required(ErrorMessage = "Id Utilizador is Required")]
        public int IdUtilizador { get; set; }

        [Column("nome")]
        [DisplayName("Username")]
        [StringLength(50)]
        [Required(ErrorMessage = "Username is Required!")]
        public string Nome { get; set; } = null!;

        [Column("telefone")]
        [DisplayName("Telefone")]
        [StringLength(20)]
        public string? Telefone { get; set; }

        [InverseProperty("IdUtilizadorNavigation")]
        public virtual ICollection<Compra> Compras { get; set; }

        [InverseProperty("IdUtilizadorNavigation")]
        public virtual ICollection<Favorito> Favoritos { get; set; }
    }
}
