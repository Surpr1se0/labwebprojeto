﻿using System;
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

            if (!String.IsNullOrEmpty(searchString))
            {
                consolas = consolas.Where(j => j.Nome!.Contains(searchString));
            }

            return View(await consolas.ToListAsync());
        }

        // GET: Consolas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consolas == null)
            {
                return NotFound();
            }

            var consola = await _context.Consolas
                .FirstOrDefaultAsync(m => m.IdConsola == id);
            if (consola == null)
            {
                return NotFound();
            }

            return View(consola);
        }

        // GET: Consolas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consolas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdConsola,Nome")] Consola consola)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consola);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consola);
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
            return View(consola);
        }

        // POST: Consolas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConsola,Nome")] Consola consola)
        {
            if (id != consola.IdConsola)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consola);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsolaExists(consola.IdConsola))
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
            return View(consola);
        }

        // GET: Consolas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consolas == null)
            {
                return NotFound();
            }

            var consola = await _context.Consolas
                .FirstOrDefaultAsync(m => m.IdConsola == id);
            if (consola == null)
            {
                return NotFound();
            }

            return View(consola);
        }

        // POST: Consolas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consolas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Consolas'  is null.");
            }
            var consola = await _context.Consolas.FindAsync(id);
            if (consola != null)
            {
                _context.Consolas.Remove(consola);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsolaExists(int id)
        {
          return _context.Consolas.Any(e => e.IdConsola == id);
        }
    }
}
