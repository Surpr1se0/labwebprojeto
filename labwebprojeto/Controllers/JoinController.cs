using labwebprojeto.Data;
using labwebprojeto.Models;
using labwebprojeto.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace labwebprojeto.Controllers
{
    public class JoinController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JoinController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Podemos adicionar um search para as 3 caixas
        public IActionResult Index()
        {
            var cat = (from c in _context.Categoria
                      select c).ToList();

            var cons = (from c in _context.Consolas
                       select c).ToList();

            var prod = (from p in _context.Produtoras
                    select p).ToList();


            var tuple = new Tuple<List<Categoria>, List<Consola>, List<Produtora>>(cat, cons, prod);

            return View(tuple);
        }
    }
}
