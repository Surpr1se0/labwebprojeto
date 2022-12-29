namespace labwebprojeto.ViewModels
{
    public class CreateJogoViewModel
    {
        public int IdJogos { get; set; }
        public string Nome { get; set; } = null!;
        public IFormFile Foto { get; set; } = null!;
        public IFormFile Foto1 { get; set; } = null!;
        public IFormFile Foto2 { get; set; } = null!;
        public int IdCategoria { get; set; }
        public int IdConsola { get; set; }
        public int IdProdutora { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = null!;
        public string Descricao1 { get; set; } = null!;
    }
}
