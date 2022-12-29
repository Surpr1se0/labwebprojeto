using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using labwebprojeto.Data;
using labwebprojeto.Models;

namespace labwebprojeto.Controllers
{
    public class JogosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JogosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jogos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Jogos.Include(j => j.IdCategoriaNavigation).Include(j => j.IdConsolaNavigation).Include(j => j.IdProdutoraNavigation);
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

            return View(jogo);
        }

        // GET: Jogos/Create
        public IActionResult Create()
        {
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
        public async Task<IActionResult> Create([Bind("IdJogos,Nome,Foto,Foto1,Foto2,IdCategoria,IdConsola,IdProdutora,Preco,Descricao,Descricao1")] Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", jogo.IdCategoria);
            ViewData["IdConsola"] = new SelectList(_context.Consolas, "IdConsola", "IdConsola", jogo.IdConsola);
            ViewData["IdProdutora"] = new SelectList(_context.Produtoras, "IdProdutora", "IdProdutora", jogo.IdProdutora);
            return View(jogo);
        }

        // GET: Jogos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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
