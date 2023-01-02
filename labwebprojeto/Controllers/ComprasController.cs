using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using labwebprojeto.Data;
using labwebprojeto.Models;
using System.Security.Claims;
using labwebprojeto.ViewModels;

namespace labwebprojeto.Controllers
{
    public class ComprasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Compras.Include(c => c.IdJogoNavigation).Include(c => c.IdUtilizadorNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdJogoNavigation)
                .Include(c => c.IdUtilizadorNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["IdJogo"] = new SelectList(_context.Jogos, "IdJogos", "Nome");
            ViewData["IdUtilizador"] = new SelectList(_context.Utilizadors, "IdUtilizador", "Nome");
            return View();
        }

        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompra,IdJogo,IdUtilizador,DataCompra")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdJogo"] = new SelectList(_context.Jogos, "IdJogos", "Nome", compra.IdJogo);
            ViewData["IdUtilizador"] = new SelectList(_context.Utilizadors, "IdUtilizador", "Nome", compra.IdUtilizador);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["IdJogo"] = new SelectList(_context.Jogos, "IdJogos", "Nome", compra.IdJogo);
            ViewData["IdUtilizador"] = new SelectList(_context.Utilizadors, "IdUtilizador", "Nome", compra.IdUtilizador);
            return View(compra);
        }

        // POST: Compras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCompra,IdJogo,IdUtilizador,DataCompra")] Compra compra)
        {
            if (id != compra.IdCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.IdCompra))
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
            ViewData["IdJogo"] = new SelectList(_context.Jogos, "IdJogos", "Nome", compra.IdJogo);
            ViewData["IdUtilizador"] = new SelectList(_context.Utilizadors, "IdUtilizador", "Nome", compra.IdUtilizador);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdJogoNavigation)
                .Include(c => c.IdUtilizadorNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Compras == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Compras'  is null.");
            }
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
          return _context.Compras.Any(e => e.IdCompra == id);
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Checkout(int? id)
        {
            var userName = GetCurrentUserName();
            var jogoName = GetCurrentJogoName(id);
            var jogoPreco = GetCurrentJogoPreco(id);

            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos.FindAsync(id);

            if (jogo == null)
            {
                return NotFound();
            }

            ViewBag.NomeJogo = jogoName;
            ViewBag.PrecoJogo= jogoPreco;
            ViewData["NomeUtilizador"] = userName;
            ViewData["IdJogo"] = new SelectList(_context.Jogos, "IdJogos", "Nome");
            return View();
        }

        // POST: Compras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(int id, CheckoutViewModel checkVM)
        {
            var user = GetCurrentUser();
            var userID = GetCurrentUserID();
            var userName = GetCurrentUserName();

            if (ModelState.IsValid)
            {
                var checkout = new Compra
                {
                    IdCompra = checkVM.IdCompra,
                    IdJogo = id,
                    IdUtilizador = userID
                };
                checkVM.IdUtilizador = userID;
                checkVM.IdJogo = id;

                _context.Add(checkVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NomeUtilizador"] = userName;

            return View(checkVM);
        }


        /*------------USER---------------*/
        public IQueryable<Utilizador> GetCurrentUser()
        {
            var user = (from u in _context.Utilizadors
                        select u);
            //Fetch Actual Identity
            var identity = (ClaimsIdentity)User.Identity;

            //Fetch of First User With Name Specified - returns ID
            //var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //Email of Identity
            var claims = identity.Name;

            //Utilizador objet where Email = Actual Email
            var userlog = user.Where(x => x.Nome.Equals(claims));

            return (userlog);
        }

        public int GetCurrentUserID()
        {
            var user = (from u in _context.Utilizadors
                        select u);
            //Fetch Actual Identity
            var identity = (ClaimsIdentity)User.Identity;

            //Fetch of First User With Name Specified - returns ID
            //var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //Email of Identity
            var claims = identity.Name;

            //Utilizador objet where Email = Actual Email
            var userlog = user.Where(x => x.Nome.Equals(claims));

            var userID = (from u in _context.Utilizadors
                          join i in userlog
                          on u.IdUtilizador equals i.IdUtilizador
                          select i.IdUtilizador).First();
            return (userID);
        }

        public List<string>? GetCurrentUserName()
        {
            var user = GetCurrentUser();
            var userName = ((from u in _context.Utilizadors
                             join f in user
                             on u.IdUtilizador equals f.IdUtilizador
                             select u.Nome).ToList());

            return userName;
        }

        /*------------JOGOS---------------*/
        public IQueryable<Jogo> GetCurrentJogo(int? id)
        {
            var jogo = (from j in _context.Jogos
                        select j)
                        .Where(x => x.IdJogos == id);

            return jogo;
        }

        public int GetCurrentJogoID(int? id)
        {
            var jogoID = (from j in _context.Jogos
                        select j.IdJogos)
            .Where(x => x == id).First();

            return jogoID;
        }

        public string GetCurrentJogoName(int? id)
        {
            var jogo = GetCurrentJogo(id);

            var name = (from j in jogo
                       select j.Nome).FirstOrDefault().ToString();

            return name;
        }

        public string GetCurrentJogoPreco(int? id)
        {
            var jogo = GetCurrentJogo(id);

            var preco = (from j in jogo
                        select j.Preco)
                        .First().ToString();

            return preco;
        }
    }
}
