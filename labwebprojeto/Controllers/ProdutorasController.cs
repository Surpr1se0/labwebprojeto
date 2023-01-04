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
    public class ProdutorasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutorasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produtoras
        public async Task<IActionResult> Index(string searchString)
        {
            var produtoras = from c in _context.Produtoras
                             select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                produtoras = produtoras.Where(j => j.Nome!.Contains(searchString));
                bool isEmpty = !produtoras.Any();
                if (isEmpty)
                {
                    //Mostrar Mensagem com ViewData
                }
            }
            return View(await produtoras.ToListAsync());
        }

        // POST: Produtoras/Create
        public async Task<IActionResult> Create([Bind("IdProdutora,Nome")] Produtora produtora, string NewName)
        {
            produtora.Nome = NewName;
            _context.Add(produtora);
            await _context.SaveChangesAsync();
            return PartialView("Listing", _context.Produtoras);
        }

        // GET: Produtoras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtoras == null)
            {
                return NotFound();
            }

            var produtora = await _context.Produtoras.FindAsync(id);
            if (produtora == null)
            {
                return NotFound();
            }
            return PartialView("Edit", produtora);
        }

        // POST: Produtoras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Edit(int id, [Bind("IdProdutora,Nome")] Produtora produtora)
        {
            if (id != produtora.IdProdutora)
            {
                return null;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtora);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoraExists(produtora.IdProdutora))
                    {
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return produtora.Nome;
        }

        // GET: Produtoras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else if (_context.Produtoras == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Produtora' is null.");
            }

            var produtora = await _context.Produtoras
                .FirstOrDefaultAsync(m => m.IdProdutora == id);
            if (produtora == null)
            {
                return NotFound();
            }

            _context.Produtoras.Remove(produtora);
            _context.SaveChanges();

            return PartialView("Listing", _context.Produtoras);
        }

        private bool ProdutoraExists(int id)
        {
          return _context.Produtoras.Any(e => e.IdProdutora == id);
        }
    }
}
