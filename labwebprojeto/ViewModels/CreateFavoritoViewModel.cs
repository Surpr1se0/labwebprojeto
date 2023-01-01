using System.ComponentModel.DataAnnotations.Schema;

namespace labwebprojeto.ViewModels
{
    public class CreateFavoritoViewModel
    {
        public int IdFavorito { get; set; }

        public int IdCategoria { get; set; }

        public int IdUtilizador { get; set; }
    }
}
