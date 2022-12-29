using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace labwebprojeto.Models
{
    [Table("Jogo")]
    public class Jogo
    {
        public Jogo()
        {
            Compras = new HashSet<Compra>();
        }

        [Key]
        [Column("Id_Jogos")]
        public int IdJogos { get; set; }
        [Column("nome")]
        [StringLength(70)]
        public string Nome { get; set; } = null!;
        [Column("foto")]
        [StringLength(100)]
        public string Foto { get; set; } = null!;
        [Column("foto1")]
        [StringLength(100)]
        public string Foto1 { get; set; } = null!;
        [Column("foto_2")]
        [StringLength(100)]
        public string Foto2 { get; set; } = null!;
        [Column("Id_Categoria")]
        public int IdCategoria { get; set; }
        [Column("Id_Consola")]
        public int IdConsola { get; set; }
        [Column("Id_Produtora")]
        public int IdProdutora { get; set; }
        [Column("preco", TypeName = "money")]
        public decimal Preco { get; set; }
        [Column("descricao")]
        [StringLength(100)]
        public string Descricao { get; set; } = null!;
        [Column("descricao1")]
        [StringLength(200)]
        public string Descricao1 { get; set; } = null!;

        [ForeignKey("IdCategoria")]
        [InverseProperty("Jogos")]
        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
        [ForeignKey("IdConsola")]
        [InverseProperty("Jogos")]
        public virtual Consola IdConsolaNavigation { get; set; } = null!;
        [ForeignKey("IdProdutora")]
        [InverseProperty("Jogos")]
        public virtual Produtora IdProdutoraNavigation { get; set; } = null!;
        [InverseProperty("IdJogoNavigation")]
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
