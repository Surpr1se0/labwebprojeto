﻿using System;
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
using labwebprojeto.Services.Interfaces;

namespace labwebprojeto.Controllers
{
    public class ComprasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;


        public ComprasController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var userObj = GetCurrentUser();
            var user = GetCurrentUserName();
            ViewData["NomeUser"] = user;

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

        private bool CompraExists(int id)
        {
          return _context.Compras.Any(e => e.IdCompra == id);
        }

        // GET: Compras/Checkout/5
        public IActionResult Checkout(int? id)
        {
            var userName = GetCurrentUserName();
            var jogoName = GetCurrentJogoName(id);
            var jogoPreco = GetCurrentJogoPreco(id);

            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var jogo = _context.Jogos.Find(id);

            if (jogo == null)
            {
                return NotFound();
            }

            ViewBag.NomeJogo = jogoName;
            ViewBag.PrecoJogo= jogoPreco;
            ViewData["NomeUtilizador"] = userName;
            ViewData["IdJogo"] = new SelectList(_context.Jogos, "IdJogos", "Nome");
            ViewData["IdJogo"] = new SelectList(_context.Jogos, "IdJogos", "Nome");
            ViewData["IdUtilizador"] = new SelectList(_context.Utilizadors, "IdUtilizador", "Nome");
            return View();
        }

        // POST: Compras/Checkout/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(int id, CheckoutViewModel checkVM)
        {
            var user = GetCurrentUser();
            var userID = GetCurrentUserID();
            var userName = GetCurrentUserName();
            var jogoName = GetCurrentJogoName(id);
            var jogoPreco = GetCurrentJogoPreco(id);

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

                _context.Add(checkout);
                TempData["Success"] = "Game Bought Successfully";

                //Actual User
                var userClients = (from u in _context.Utilizadors
                                   select u)
                                  .Where(x => x.IsCliente == true 
                                  && x.IdUtilizador == userID);
                //Email do Cliente Atual 
                var emailClients = (from u in userClients
                                    select u.Email);

                //Envio do Email
                foreach (var c in emailClients)
                { //foto nao funciona
                    await _emailService.SendEmailAsync(c, "Agradecemos-te pela tua compra! - " + jogoName, 
                        "Olá! Agradecemos-te pela tua recente transação na Loja. Os artigos" +
                        " abaixo foram adicionados à tua lista de compra, onde os poderás visualizar." +
                        "<br>" + jogoName + " - " + jogoPreco +"." +
                        "<br> <img src=wwwroot/Images/logo.png>"
                        );
                }
                TempData["Error"] = "Game Not Bought Successfully";

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
                       select j.Nome).
                       FirstOrDefault().
                       ToString();

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
