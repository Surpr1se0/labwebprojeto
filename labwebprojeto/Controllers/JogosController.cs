using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using labwebprojeto.Data;
using labwebprojeto.Models;
using labwebprojeto.Services.Interfaces;
using labwebprojeto.ViewModels;

namespace labwebprojeto.Controllers
{
    public class JogosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;
        private readonly IEmailService _emailService;

        public JogosController(ApplicationDbContext context, 
            IPhotoService photoService, 
            IEmailService emailService)
        {
            _context = context;
            _photoService = photoService;
            _emailService = emailService;
        }

        // GET: Jogos
        public async Task<IActionResult> Index()
        {
            //Show Category, Consola and Produtora Name
            var categoria = _context.Jogos
                .Include(x => x.Categorias)
                .Distinct()
                .ToList();

            var consola = _context.Jogos
                .Include(x => x.Consolas)
                .Distinct()
                .ToList();

            var produtora = _context.Jogos
                .Include(x => x.Produtoras)
                .Distinct()
                .ToList();

            ViewData["Categoria"] = categoria;
            ViewData["Consola"] = consola;
            ViewData["Produtora"] = produtora;

            var applicationDbContext = _context.Jogos.
                Include(j => j.IdCategoriaNavigation)
                .Include(j => j.IdConsolaNavigation)
                .Include(j => j.IdProdutoraNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Jogos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jogos == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos
                .Include(j => j.IdCategoriaNavigation)
                .Include(j => j.IdConsolaNavigation)
                .Include(j => j.IdProdutoraNavigation)
                .FirstOrDefaultAsync(m => m.IdJogos == id);
            if (jogo == null)
            {
                return NotFound();
            }

            //Show Category, Consola and Produtora Name
            var categoria = _context.Jogos
                .Include(x => x.Categorias)
                .Distinct()
                .ToList();

            var consola = _context.Jogos
                .Include(x => x.Consolas)
                .Distinct()
                .ToList();

            var produtora = _context.Jogos
                .Include(x => x.Produtoras)
                .Distinct()
                .ToList();

            ViewData["Categoria"] = categoria;
            ViewData["Consola"] = consola;
            ViewData["Produtora"] = produtora;

            return View(jogo);
        }

        // GET: Jogos/Create
        public IActionResult Create()
        {
            //Show Category, Consola and Produtora Name
            var categoria = _context.Jogos
                .Include(x => x.Categorias)
                .Distinct()
                .ToList();

            var consola = _context.Jogos
                .Include(x => x.Consolas)
                .Distinct()
                .ToList();

            var produtora = _context.Jogos
                .Include(x => x.Produtoras)
                .Distinct()
                .ToList();

            ViewData["Categoria"] = categoria;
            ViewData["Consola"] = consola;
            ViewData["Produtora"] = produtora;

            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria");
            ViewData["IdConsola"] = new SelectList(_context.Consolas, "IdConsola", "IdConsola");
            ViewData["IdProdutora"] = new SelectList(_context.Produtoras, "IdProdutora", "IdProdutora");
            return View();
        }

        // POST: Jogos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateJogoViewModel jogoVM)
        {
            if (ModelState.IsValid)
            {
                var result_pic = await _photoService.AddPhotoAsync(jogoVM.Foto);
                var result_pic1 = await _photoService.AddPhotoAsync(jogoVM.Foto1);
                var result_pic2 = await _photoService.AddPhotoAsync(jogoVM.Foto2);
                var jogo = new Jogo
                {
                    IdJogos = jogoVM.IdJogos,
                    Nome = jogoVM.Nome,
                    Foto = result_pic.Url.ToString(),
                    Foto1 = result_pic1.Url.ToString(),
                    Foto2 = result_pic2.Url.ToString(),
                    IdCategoria = jogoVM.IdCategoria,
                    IdConsola = jogoVM.IdConsola,
                    IdProdutora = jogoVM.IdProdutora,
                    Preco = Math.Round(jogoVM.Preco, 2),
                    Descricao = jogoVM.Descricao,
                    Descricao1 = jogoVM.Descricao1,
                };
                _context.Add(jogo);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Game Created Successfully";

                //Add Email Sender Notification - Problem
                var email = _context.Utilizadors
                    .Include(x => x.Email)
                    .ToString();
                await _emailService.SendEmailAsync(email, "New Game", "New game added!");

                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Game Created Successfully";
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", jogoVM.IdCategoria);
            ViewData["IdConsola"] = new SelectList(_context.Consolas, "IdConsola", "IdConsola", jogoVM.IdConsola);
            ViewData["IdProdutora"] = new SelectList(_context.Produtoras, "IdProdutora", "IdProdutora", jogoVM.IdProdutora);

            return View(jogoVM);
        }

        // GET: Jogos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //Show Category, Consola and Produtora Name
            var categoria = _context.Jogos
                .Include(x => x.Categorias)
                .Distinct()
                .ToList();

            var consola = _context.Jogos
                .Include(x => x.Consolas)
                .Distinct()
                .ToList();

            var produtora = _context.Jogos
                .Include(x => x.Produtoras)
                .Distinct()
                .ToList();
            ViewData["Categoria"] = categoria;
            ViewData["Consola"] = consola;
            ViewData["Produtora"] = produtora;

            if (id == null || _context.Jogos == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", jogo.IdCategoria);
            ViewData["IdConsola"] = new SelectList(_context.Consolas, "IdConsola", "IdConsola", jogo.IdConsola);
            ViewData["IdProdutora"] = new SelectList(_context.Produtoras, "IdProdutora", "IdProdutora", jogo.IdProdutora);
            return View(jogo);
        }

        // POST: Jogos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdJogos,Nome,Foto,Foto1,Foto2,IdCategoria,IdConsola,IdProdutora,Preco,Descricao,Descricao1")] Jogo jogo)
        {
            if (id != jogo.IdJogos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogoExists(jogo.IdJogos))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", jogo.IdCategoria);
            ViewData["IdConsola"] = new SelectList(_context.Consolas, "IdConsola", "IdConsola", jogo.IdConsola);
            ViewData["IdProdutora"] = new SelectList(_context.Produtoras, "IdProdutora", "IdProdutora", jogo.IdProdutora);
            return View(jogo);
        }

        // GET: Jogos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //Show Category, Consola and Produtora Name
            var categoria = _context.Jogos
                .Include(x => x.Categorias)
                .Distinct()
                .ToList();

            var consola = _context.Jogos
                .Include(x => x.Consolas)
                .Distinct()
                .ToList();

            var produtora = _context.Jogos
                .Include(x => x.Produtoras)
                .Distinct()
                .ToList();
            ViewData["Categoria"] = categoria;
            ViewData["Consola"] = consola;
            ViewData["Produtora"] = produtora;

            if (id == null || _context.Jogos == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos
                .Include(j => j.IdCategoriaNavigation)
                .Include(j => j.IdConsolaNavigation)
                .Include(j => j.IdProdutoraNavigation)
                .FirstOrDefaultAsync(m => m.IdJogos == id);
            if (jogo == null)
            {
                return NotFound();
            }

            return View(jogo);
        }

        // POST: Jogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jogos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Jogos'  is null.");
            }
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo != null)
            {
                _context.Jogos.Remove(jogo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogoExists(int id)
        {
          return _context.Jogos.Any(e => e.IdJogos == id);
        }
    }
}
