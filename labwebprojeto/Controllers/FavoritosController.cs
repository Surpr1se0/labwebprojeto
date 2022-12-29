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
    public class FavoritosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoritosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Favoritos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Favoritos.Include(f => f.IdCategoriaNavigation).Include(f => f.IdUtilizadorNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Favoritos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Favoritos == null)
            {
                return NotFound();
            }

            var favorito = await _context.Favoritos
                .Include(f => f.IdCategoriaNavigation)
                .Include(f => f.IdUtilizadorNavigation)
                .FirstOrDefaultAsync(m => m.IdFavorito == id);
            if (favorito == null)
            {
                return NotFound();
            }

            return View(favorito);
        }

        // GET: Favoritos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria");
            ViewData["IdUtilizador"] = new SelectList(_context.Utilizadors, "IdUtilizador", "Nome");
            return View();
        }

        // POST: Favoritos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFavorito,IdCategoria,IdUtilizador")] Favorito favorito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favorito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", favorito.IdCategoria);
            ViewData["IdUtilizador"] = new SelectList(_context.Utilizadors, "IdUtilizador", "Nome", favorito.IdUtilizador);
            return View(favorito);
        }

        // GET: Favoritos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Favoritos == null)
            {
                return NotFound();
            }

            var favorito = await _context.Favoritos.FindAsync(id);
            if (favorito == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", favorito.IdCategoria);
            ViewData["IdUtilizador"] = new SelectList(_context.Utilizadors, "IdUtilizador", "Nome", favorito.IdUtilizador);
            return View(favorito);
        }

        // POST: Favoritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFavorito,IdCategoria,IdUtilizador")] Favorito favorito)
        {
            if (id != favorito.IdFavorito)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favorito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoritoExists(favorito.IdFavorito))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", favorito.IdCategoria);
            ViewData["IdUtilizador"] = new SelectList(_context.Utilizadors, "IdUtilizador", "Nome", favorito.IdUtilizador);
            return View(favorito);
        }

        // GET: Favoritos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Favoritos == null)
            {
                return NotFound();
            }

            var favorito = await _context.Favoritos
                .Include(f => f.IdCategoriaNavigation)
                .Include(f => f.IdUtilizadorNavigation)
                .FirstOrDefaultAsync(m => m.IdFavorito == id);
            if (favorito == null)
            {
                return NotFound();
            }

            return View(favorito);
        }

        // POST: Favoritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Favoritos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Favoritos'  is null.");
            }
            var favorito = await _context.Favoritos.FindAsync(id);
            if (favorito != null)
            {
                _context.Favoritos.Remove(favorito);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoritoExists(int id)
        {
          return _context.Favoritos.Any(e => e.IdFavorito == id);
        }
    }
}
