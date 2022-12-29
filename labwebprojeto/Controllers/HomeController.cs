using labwebprojeto.Data;
using labwebprojeto.Models;
using labwebprojeto.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace labwebprojeto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IPhotoService photoService)
        {
            _logger = logger;
            _context = context;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var jogos = from j in _context.Jogos
                        select j;

            // Search 
            if (!String.IsNullOrEmpty(searchString))
            {
                jogos = jogos.Where(j => j.Nome!.Contains(searchString));
            }

            // Show Category, Consola and Produtora Name
            var categoria = (from j in _context.Jogos
                             join c in _context.Categoria on j.IdCategoria equals c.IdCategoria
                             select c.Nome).Distinct().ToList();
            ViewData["Categoria"] = categoria;

            var consola = (from j in _context.Jogos
                           join c in _context.Consolas on j.IdCategoria equals c.IdConsola
                           select c.Nome).Distinct().ToList();
            ViewData["Consola"] = consola;

            var produtora = (from j in _context.Jogos
                             join c in _context.Produtoras on j.IdCategoria equals c.IdProdutora
                             select c.Nome).Distinct().ToList();
            ViewData["Produtora"] = produtora;

            return View(await jogos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jogos == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos
                .FirstOrDefaultAsync(m => m.IdJogos == id);
            if (jogo == null)
            {
                return NotFound();
            }

            //Show Category, Consola and Produtora Name
            var categoria = (from j in _context.Jogos
                             join c in _context.Categoria on j.IdCategoria equals c.IdCategoria
                             select c.Nome).Distinct().ToList();
            ViewData["Categoria"] = categoria;

            var consola = (from j in _context.Jogos
                           join c in _context.Consolas on j.IdCategoria equals c.IdConsola
                           select c.Nome).Distinct().ToList();
            ViewData["Consola"] = consola;

            var produtora = (from j in _context.Jogos
                             join c in _context.Produtoras on j.IdCategoria equals c.IdProdutora
                             select c.Nome).Distinct().ToList();
            ViewData["Produtora"] = produtora;

            return View(jogo);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}