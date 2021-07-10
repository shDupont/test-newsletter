using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newsletter.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Newsletter.Controllers
{
    public class HomeController : Controller
    {

        private readonly ClientesContext _context;
        public HomeController(ClientesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {           
            return View();
        }
        public IActionResult Login()
        {
            
            return View();
        }

        

        public IActionResult AdicionarOuEditar(int id = 0)
        {
            if (id == 0)
                return View(new Clientes());
            else
                return View(_context.Clientes.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarOuEditar([Bind("id, nomeCompleto, email, ativo, dataCadastro, dataRevogação")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                if (clientes.id == 0)
                    _context.Add(clientes);
                else
                    _context.Update(clientes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientes);
        }

        public async Task<IActionResult> Criar([Bind("id, nomeCompleto, email, ativo, dataCadastro, dataRevogação")] Clientes clientes)
        {
            
            _context.Add(clientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
