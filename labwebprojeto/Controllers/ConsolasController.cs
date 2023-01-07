using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using labwebprojeto.Data;
using labwebprojeto.Models;
using Microsoft.AspNetCore.Authorization;

namespace labwebprojeto.Controllers
{
    [Authorize(Roles = "Func, Admin")]
    public class ConsolasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsolasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consolas
        public async Task<IActionResult> Index(string searchString)
        {
            var consolas = from c in _context.Consolas
                             select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                consolas = consolas.Where(j => j.Nome!.Contains(searchString));
                bool isEmpty = !consolas.Any();
                if (isEmpty)
                {
                    ViewData["empty_message"] = "There are no results...";
                }
                else
                {
                    ViewData["empty_message"] = "";
                }
            }
            return View(await consolas.ToListAsync());
        }

        // POST: Consolas/Create
        public async Task<IActionResult> Create([Bind("IdConsola,Nome")] Consola consola, string NewName)
        {
            consola.Nome = NewName;
            _context.Add(consola);
            await _context.SaveChangesAsync();
            return PartialView("Listing", _context.Consolas);
        }

        // GET: Consolas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consolas == null)
            {
                return NotFound();
            }

            var consola = await _context.Consolas.FindAsync(id);
            if (consola == null)
            {
                return NotFound();
            }
            return PartialView("Edit", consola);
        }

        // POST: Consolas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Edit(int id, [Bind("IdConsola,Nome")] Consola consola)
        {
            if (id != consola.IdConsola)
            {
                return null;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consola);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsolaExists(consola.IdConsola))
                    {
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return consola.Nome;
        }

        // GET: Consolas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else if (_context.Consolas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Consolas' is null.");
            }

            var consola = await _context.Consolas
                .FirstOrDefaultAsync(m => m.IdConsola == id);
            if (consola == null)
            {
                return NotFound();
            }

            _context.Consolas.Remove(consola);
            _context.SaveChanges();

            return PartialView("Listing", _context.Consolas);
        }


        private bool ConsolaExists(int id)
        {
          return _context.Consolas.Any(e => e.IdConsola == id);
        }
    }
}
