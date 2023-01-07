using labwebprojeto.Data;
using labwebprojeto.Models;
using labwebprojeto.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace labwebprojeto.Controllers
{
    [AllowAnonymous]
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

        public async Task<IActionResult> Index()
        {
            var jogos = from j in _context.Jogos
                        select j;

            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nome");
            ViewData["IdConsola"] = new SelectList(_context.Consolas, "IdConsola", "Nome");
            ViewData["IdProdutora"] = new SelectList(_context.Produtoras, "IdProdutora", "Nome");

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

            return View(jogo);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}