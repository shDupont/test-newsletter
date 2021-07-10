using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newsletter.Models;

namespace Newsletter.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClientesContext _context;

        public async Task<IActionResult> Index(string searchString)
        {
            var clientes = from m in _context.Clientes
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(s => s.email.Contains(searchString) || s.nomeCompleto.Contains(searchString));
            }

            return View(await clientes.ToListAsync());
        }

        public ClienteController(ClientesContext context)
        {
            _context = context;
        }

        // GET: Cliente
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Clientes.ToListAsync());
        //}

        // GET: Cliente/Create
        public IActionResult AdicionarOuEditar(int id=0)
        {
            if (id == 0) 
                return View(new Clientes()); 
            else
                return View(_context.Clientes.Find(id));
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarOuEditar([Bind("id,nomeCompleto,email,ativo,dataCadastro,dataRevogação")] Clientes clientes)
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
        
        // GET: Cliente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }

    
}
