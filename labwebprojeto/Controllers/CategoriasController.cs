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
    //In the future:[Authorize(Roles="Func, Admin")]
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index(string searchString)
        {
            var categorias = from c in _context.Categoria
                             select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                categorias = categorias.Where(j => j.Nome!.Contains(searchString));
                bool isEmpty = !categorias.Any();
                if (isEmpty)
                {
                    //Mostrar Mensagem com ViewData
                }
            }
            return View(await categorias.ToListAsync());
        }

        // POST: Categorias/Create
        public async Task<IActionResult> Create([Bind("IdCategoria,Nome")] Categoria categoria, string NewName)
        {
            categoria.Nome = NewName;
            _context.Add(categoria);
            await _context.SaveChangesAsync();
            return PartialView("Listing", _context.Categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categoria == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return PartialView("Edit", categoria);
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Edit(int id, [Bind("IdCategoria,Nome")] Categoria categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return null;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.IdCategoria))
                    {
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return categoria.Nome;
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else if(_context.Categoria == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categoria' is null.");
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categoria.Remove(categoria);
            _context.SaveChanges();

            return PartialView("Listing", _context.Categoria);
        }

        private bool CategoriaExists(int id)
        {
          return _context.Categoria.Any(e => e.IdCategoria == id);
        }
    }
}
