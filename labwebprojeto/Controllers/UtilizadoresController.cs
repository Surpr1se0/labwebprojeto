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
    [Authorize(Roles ="Admin")]
    public class UtilizadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UtilizadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Utilizadores
        public async Task<IActionResult> Index(string searchString)
        {
            var list = (from u in _context.Utilizadors
                        select u)
                       .Where(x => x.IsFunc || x.IsAdmin == true);

            if (!string.IsNullOrEmpty(searchString))
            {
                list = list
                    .Where(j => j.Nome!.Contains(searchString));
                bool isEmpty = !list.Any();
                if (isEmpty)
                {
                    ViewData["empty_message"] = "There are no results...";
                }
                else
                {
                    ViewData["empty_message"] = "";
                }
            }
                return View(await list.ToListAsync());
        }

        // GET: Utilizadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Utilizadors == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadors
                .FirstOrDefaultAsync(m => m.IdUtilizador == id);
            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }
    }
}
