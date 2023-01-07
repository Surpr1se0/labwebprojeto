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
using Syncfusion.EJ2.Linq;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var nome = GetCurrentUserName();
            ViewData["userNome"] = nome;

            var applicationDbContext = _context.Favoritos
                .Include(f => f.IdCategoriaNavigation)
                .Include(f => f.IdUtilizadorNavigation)
                .Where(f => f.IdUtilizadorNavigation.IdUtilizador
                == GetCurrentUserID());

            bool isEmpty = !applicationDbContext.Any();
            if (isEmpty)
            {
                //Mostrar Mensagem com ViewData
            }

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Favoritos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Favoritos == null)
            {
                return NotFound();
            }
            var userID = GetCurrentUserID();

            var jogosUti = (from f in _context.Favoritos
                            join u in _context.Utilizadors
                            on f.IdUtilizador equals userID
                            select f)
                            .Where(x=>x.IdFavorito == id)
                            .Distinct();

            var jogos = from j in _context.Jogos
                        join f in jogosUti
                        on j.IdCategoria equals f.IdCategoria
                        select j.Nome;

            ViewData["ListaJogos"] = jogos;

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
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nome");
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
                //verificar se nao está mal
                    //Acho que deveria ser ao contrario
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

            //Utilizador objet where Nome = Actual Nome
            var userlog = user.Where(x => x.Nome.Equals(claims));

            var userID = (from u in _context.Utilizadors
                         join i in userlog
                         on u.IdUtilizador equals i.IdUtilizador
                         select i.IdUtilizador).First();
            return (userID);
        }

        public string? GetCurrentUserName()
        {
            var user = (from u in _context.Utilizadors
                        select u);
            //Fetch Actual Identity
            var identity = (ClaimsIdentity)User.Identity;

            //Email of Identity
            var claims = identity.Name;

            return claims;
        }

    }
}
