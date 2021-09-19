using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using teste.Data;
using teste.Models;

namespace teste.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        /// <summary>
        /// variável que identifica a BD do projeto
        /// </summary>
        private readonly Teste _context;

        private readonly IWebHostEnvironment _caminho;

        private readonly UserManager<ApplicationUser> _userManager;

        public ClientesController(Teste context, IWebHostEnvironment caminho, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }

        // GET: Clientes
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var util = await _context.Clientes.FirstOrDefaultAsync(r => r.Username == user.Id);
                

                return View(await _context.Clientes.ToListAsync());
            }
            catch { return View(await _context.Clientes.ToListAsync()); }
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.Include(r => r.ListaDeReservas).Where(c => c.Id == id)
                                                   .FirstOrDefaultAsync();
            if (clientes == null)
            {
                return NotFound();
            }
            var cliente = await _userManager.GetUserAsync(User);
            var util = await _context.Clientes.FirstOrDefaultAsync(r => r.Username == cliente.Id);

            return View(clientes);
        }

        // GET: Clientes/Create
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Contacto,Datanasc,Fotografia")] Clientes cliente, IFormFile fotoCliente)
        {
            string caminhoCompleto = "";
            bool himage = false;

            if (fotoCliente == null) { cliente.Fotografia = "noUser.jpg"; }
            else
            {
                if (fotoCliente.ContentType == "image/jpeg" || fotoCliente.ContentType == "image/jpg" || fotoCliente.ContentType == "image/png")
                {
                   
                    Guid g;
                    g = Guid.NewGuid();
                    string extensao = Path.GetExtension(fotoCliente.FileName).ToLower();
                    string nome = g.ToString() + extensao;

                    
                    caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Imagens", nome);

                   
                    cliente.Fotografia = nome;

                   
                    himage = true;
                }
                else
                {
               
                    cliente.Fotografia = "noUser.jpg";
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    if (himage)
                    {
                        using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                        await fotoCliente.CopyToAsync(stream);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Contacto,Datanasc,Fotografia")] Clientes cliente, IFormFile fotoCliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            string caminhoCompleto = "";
            bool hImagem = false;

            if (fotoCliente == null) { cliente.Fotografia = "noUser.png"; }
            else
            {
                if (fotoCliente.ContentType == "image/jpeg" || fotoCliente.ContentType == "image/jpg" || fotoCliente.ContentType == "image/png")
                {

                    Guid g;
                    g = Guid.NewGuid();
                    string extensao = Path.GetExtension(fotoCliente.FileName).ToLower();
                    string nome = g.ToString() + extensao;


                    caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Imagens", nome);


                    cliente.Fotografia = nome;

                    hImagem = true;
                }
                else
                {

                    cliente.Fotografia = "noUser.png";
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                    if (hImagem)
                    {
                        using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                        await fotoCliente.CopyToAsync(stream);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientesExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
