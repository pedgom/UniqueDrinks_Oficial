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
    //Authorização refere-se como um processo que determina o que um utilizador é capaz de fazer  
    //Neste caso específico, apenas Clientes com conta podem aceder às Bebidas
    [Authorize]
    public class ClientesController : Controller
    {
        /// <summary>
        /// variavel para identificar a base de dados
        /// </summary>
        private readonly Teste _context;

        /// <summary>
        /// variavel que contém os dados de ficheiros guardados no servidor
        /// </summary>
        private readonly IWebHostEnvironment _caminho;

        /// <summary>
        /// variavel que recolhe os dados de um utilizador autenticado
        /// </summary>
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
                //caso o id seja nulo retorna-se à página Index dos Clientes
                return RedirectToAction("Index", "Clientes");
            }

            var clientes = await _context.Clientes.Include(r => r.ListaDeReservas).Where(c => c.Id == id)
                                                   .FirstOrDefaultAsync();
            if (clientes == null)
            {
                //caso o id seja nulo retorna-se à página Index dos Clientes
                return RedirectToAction("Index", "Clientes");
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
            // variáveis auxiliares
            string caminhoCompleto = "";
            bool himage = false;

            //caso nao exista imagem é atribuida uma imagem default
            if (fotoCliente == null) { cliente.Fotografia = "noUser.jpg"; }
            else
            {
                
                //as extensões aceites são ".jpeg"; ".jpg" e ".png"
                if (fotoCliente.ContentType == "image/jpeg" || fotoCliente.ContentType == "image/jpg" || fotoCliente.ContentType == "image/png")
                {
                    // o ficheiro é uma imagem válida
                    // preparar a imagem para ser guardada no disco rígido
                    Guid g;
                    g = Guid.NewGuid();
                    string extensao = Path.GetExtension(fotoCliente.FileName).ToLower();
                    string nome = g.ToString() + extensao;

                    // onde guardar o ficheiro
                    caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Imagens", nome);

                   
                    cliente.Fotografia = nome;

                    // assinalar que existe imagem e é preciso guardá-la no disco rígido
                    himage = true;
                }
                else
                {
                    //caso exista imagem, mas não tem uma das extensões exigidas, é atribuida uma imagem default
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
                //caso o id seja nulo retorna-se à página Index dos Clientes
                return RedirectToAction("Index", "Clientes");
            }

            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                //caso nao seja possivel identificar o cliente retorna-se à página Index dos Clientes
                return RedirectToAction("Index", "Clientes");
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
                return RedirectToAction("Index", "Clientes");
            }

            // variáveis auxiliares
            string caminhoCompleto = "";
            bool hImagem = false;


            //caso nao exista imagem é atribuida uma imagem default
            if (fotoCliente == null) { cliente.Fotografia = "noUser.png"; }
            else
            {
                //as extensões aceites são ".jpeg"; ".jpg" e ".png"
                if (fotoCliente.ContentType == "image/jpeg" || fotoCliente.ContentType == "image/jpg" || fotoCliente.ContentType == "image/png")
                {

                    // o ficheiro é uma imagem válida
                    // preparar a imagem para ser guardada no disco rígido
                    Guid g;
                    g = Guid.NewGuid();
                    string extensao = Path.GetExtension(fotoCliente.FileName).ToLower();
                    string nome = g.ToString() + extensao;

                    // onde guardar o ficheiro
                    caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Imagens", nome);


                    cliente.Fotografia = nome;
                    // assinalar que existe imagem e é preciso guardá-la no disco rígido
                    hImagem = true;
                }
                else
                {
                    //caso exista imagem, mas não tem uma das extensões exigidas, é atribuida uma imagem default
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
                        return RedirectToAction("Index", "Clientes");
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
                return RedirectToAction("Index", "Clientes");
            }

            var clientes = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientes == null)
            {
                return RedirectToAction("Index", "Clientes");
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
