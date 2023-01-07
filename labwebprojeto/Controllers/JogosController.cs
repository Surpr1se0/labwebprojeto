using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using labwebprojeto.Data;
using labwebprojeto.Models;
using labwebprojeto.Services.Interfaces;
using labwebprojeto.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace labwebprojeto.Controllers
{
    [AllowAnonymous]
    public class JogosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;
        private readonly IEmailService _emailService;

        public JogosController(ApplicationDbContext context, 
            IPhotoService photoService, 
            IEmailService emailService)
        {
            _context = context;
            _photoService = photoService;
            _emailService = emailService;
        }

        // GET: Jogos
        public async Task<IActionResult> Index(string searchString)
        {
            var jogos = from j in _context.Jogos
                        select j;

            if (!String.IsNullOrEmpty(searchString))
            {
                jogos = jogos.Where(j => j.Nome!.Contains(searchString));
                bool isEmpty = !jogos.Any();

                if (isEmpty)
                {
                    //Mostrar Mensagem com ViewData
                }
            }

            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nome");
            ViewData["IdConsola"] = new SelectList(_context.Consolas, "IdConsola", "Nome");
            ViewData["IdProdutora"] = new SelectList(_context.Produtoras, "IdProdutora", "Nome");

            var applicationDbContext = _context.Jogos.
                Include(j => j.IdCategoriaNavigation)
                .Include(j => j.IdConsolaNavigation)
                .Include(j => j.IdProdutoraNavigation);
            return View(await jogos.ToListAsync());
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

        [Authorize(Roles = "Func, Admin")]
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nome");
            ViewData["IdConsola"] = new SelectList(_context.Consolas, "IdConsola", "Nome");
            ViewData["IdProdutora"] = new SelectList(_context.Produtoras, "IdProdutora", "Nome");
            return View();
        }

        // POST: Jogos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Func, Admin")]
        public async Task<IActionResult> Create(CreateJogoViewModel jogoVM)
        {
            if (ModelState.IsValid)
            {
                var result_pic = await _photoService.AddPhotoAsync(jogoVM.Foto);
                var result_pic1 = await _photoService.AddPhotoAsync(jogoVM.Foto1);
                var background = await _photoService.AddBackgroundAsync(jogoVM.Foto2);
                var jogo = new Jogo
                {
                    IdJogos = jogoVM.IdJogos,
                    Nome = jogoVM.Nome,
                    Foto = result_pic.Url.ToString(),
                    Foto1 = result_pic1.Url.ToString(),
                    Foto2 = background.Url.ToString(),
                    IdCategoria = jogoVM.IdCategoria,
                    IdConsola = jogoVM.IdConsola,
                    IdProdutora = jogoVM.IdProdutora,
                    Preco = Math.Round(jogoVM.Preco, 2),
                    Descricao = jogoVM.Descricao,
                    Descricao1 = jogoVM.Descricao1,
                };
                _context.Add(jogo);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Game Created Successfully";

                //Send Email
                var emailClient = GetClientEmails();
                var CatClients = GetClientFavs();
                var favourites = CatClients.Where(x => x.IdCategoria == jogoVM.IdCategoria);
                if(favourites.Any())
                {
                    foreach (var c in emailClient)
                    {
                        await _emailService.SendEmailAsync(c, "New Game - " + jogoVM.Nome, "New game added!");
                    }
                }

                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Game Not Created";
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nome", jogoVM.IdCategoria);
            ViewData["IdConsola"] = new SelectList(_context.Consolas, "IdConsola", "Nome", jogoVM.IdConsola);
            ViewData["IdProdutora"] = new SelectList(_context.Produtoras, "IdProdutora", "Nome", jogoVM.IdProdutora);

            return View(jogoVM);
        }

        // GET: Jogos/Edit/5
        [Authorize(Roles = "Func, Admin")]
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
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nome", jogo.IdCategoria);
            ViewData["IdConsola"] = new SelectList(_context.Consolas, "IdConsola", "Nome", jogo.IdConsola);
            ViewData["IdProdutora"] = new SelectList(_context.Produtoras, "IdProdutora", "Nome", jogo.IdProdutora);
            return View(jogo);
        }

        // POST: Jogos/Edit/5
        [Authorize(Roles = "Func, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("IdJogos," +
            "Nome,Foto,Foto1," +
            "Foto2,IdCategoria," +
            "IdConsola,IdProdutora," +
            "Preco,Descricao,Descricao1")] Jogo jogo)
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
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nome", jogo.IdCategoria);
            ViewData["IdConsola"] = new SelectList(_context.Consolas, "IdConsola", "Nome", jogo.IdConsola);
            ViewData["IdProdutora"] = new SelectList(_context.Produtoras, "IdProdutora", "Nome", jogo.IdProdutora);
            return View(jogo);
        }

        // GET: Jogos/Delete/5
        [Authorize(Roles = "Func, Admin")]
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
        [Authorize(Roles = "Func, Admin")]
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


        /*--------CATEGORIAS*---------*/
        public async Task<IActionResult> IndexCategorias(int? id)
        {
            var jogos = (from j in _context.Jogos
                         select j)
                         .Where(x => x.IdCategoria == id);
            return View(await jogos.ToListAsync());
        }

        /*--------PRODUTORAS*---------*/
        public IActionResult IndexProdutoras(int? id)
        {
            var jogos = (from j in _context.Jogos
                         select j)
                         .Where(x => x.IdProdutora == id);
            return View(jogos.ToList());
        }

        /*--------CONSOLAS*---------*/
        public async Task<IActionResult> IndexConsolas(int? id)
        {
            var jogos = (from j in _context.Jogos
                         select j)
                         .Where(x => x.IdConsola == id);
            return View(await jogos.ToListAsync());
        }

        /*--------CONSOLAS*---------*/
        public IQueryable<string> GetClientEmails()
        {
            //Add Email Notification
            var userClients = (from u in _context.Utilizadors
                             select u)
                             .Where(x => x.IsCliente == true);

            //Lista de emails
            var emailClients = (from u in userClients
                                select u.Email);

            return emailClients;
        }

        public IQueryable<Favorito> GetClientFavs()
        {
            var userClients = (from u in _context.Utilizadors
                               select u)
                 .Where(x => x.IsCliente == true);

            //Lista de emails
            var emailClients = (from u in userClients
                                select u.Email);

            //favoritos que tem um cliente associado
            var categoryClients = (from f in _context.Favoritos
                                   join u in userClients
                                   on f.IdUtilizador equals u.IdUtilizador
                                   select f);

            return categoryClients;
        }
    }



}
