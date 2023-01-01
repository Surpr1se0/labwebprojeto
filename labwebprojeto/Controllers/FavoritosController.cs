using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using labwebprojeto.Data;
using labwebprojeto.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using CloudinaryDotNet;
using labwebprojeto.ViewModels;
using Syncfusion.EJ2.Buttons;

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
            var userName = GetCurrentUserName();

            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nome");
            ViewData["NomeUtilizador"] = userName;
            return View();
        }

        // POST: Favoritos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFavoritoViewModel favVM)
        {
            var user = GetCurrentUser();
            var userID = GetCurrentUserID();
            var userName = GetCurrentUserName();

            if (ModelState.IsValid)
            {
                var fav = new Favorito
                {
                    IdFavorito = favVM.IdFavorito,
                    IdCategoria = favVM.IdCategoria,
                    IdUtilizador = userID
                };
                userID = favVM.IdUtilizador;

                _context.Add(fav);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nome", favVM.IdCategoria);
            ViewData["NomeUtilizador"] = userName;
            return View(favVM);
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


        /*------------Utilizadores---------------*/
        public async Task<IActionResult> UtilizadorIndex()
        {
            var user = GetCurrentUser();

            //Favoritos of Actual User
            var userFavs = (from f in _context.Favoritos
                            join u in user
                            on f.IdUtilizador equals u.IdUtilizador
                            select f);

            ViewData["IdCategoria"] = new SelectList(_context.Utilizadors, "IdUtilizador", "Nome");
            ViewData["IdUtilizador"] = new SelectList(_context.Categoria, "IdCategoria", "Nome");

            return View(await userFavs.ToListAsync());
        }

        /*------------GetCurrentUer---------------*/
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

        /*------------GetCurrentID---------------*/
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

        /*------------GetCurretnUserName---------------*/
        public List<string>? GetCurrentUserName()
        {
            var user = GetCurrentUser();
            var userName = ((from u in _context.Utilizadors
                             join f in user
                             on u.IdUtilizador equals f.IdUtilizador
                             select u.Nome).ToList());

            return userName;
        }

    }
}
