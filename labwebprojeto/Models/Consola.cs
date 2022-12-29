using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace labwebprojeto.Models
{
    [Table("Consola")]
    public class Consola
    {
        public Consola()
        {
            Jogos = new HashSet<Jogo>();
        }

        [Key]
        [Column("Id_Consola")]
        [Required(ErrorMessage = "Id Categoria is Required")]
        [DisplayName("ID Consola")]
        public int IdConsola { get; set; }

        [Column("nome")]
        [StringLength(50)]
        [DisplayName("Consola")]
        public string Nome { get; set; } = null!;

        [InverseProperty("IdConsolaNavigation")]
        public virtual ICollection<Jogo> Jogos { get; set; }
    }
}
