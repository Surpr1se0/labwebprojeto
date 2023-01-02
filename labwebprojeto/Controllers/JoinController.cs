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

        public IActionResult Index(string str1, string str2, string str3)
        {

            var cat = (from c in _context.Categoria
                       select c);

            var cons = (from c in _context.Consolas
                       select c);

            var prod = (from p in _context.Produtoras
                    select p);
            
            if(!String.IsNullOrEmpty(str1))
            {
               cat = cat.Where(x => x.Nome.Contains(str1));
            }

            if (!String.IsNullOrEmpty(str2))
            {
                cons = cons.Where(x => x.Nome.Contains(str2));
            }

            if (!String.IsNullOrEmpty(str3))
            {
                prod = prod.Where(x => x.Nome.Contains(str3));
            }

            var tuple = new Tuple<List<Categoria>, List<Consola>, List<Produtora>>(cat.ToList(), cons.ToList(), prod.ToList());

            return View(tuple);
        }
    }
}
