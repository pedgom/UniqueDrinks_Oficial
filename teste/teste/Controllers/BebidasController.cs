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
    public class BebidasController : Controller
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

        public BebidasController(Teste context, IWebHostEnvironment caminho, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }

        // GET: Bebidas
        /// <summary>
        /// lista os dados das bebidas
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Clientes.FirstOrDefaultAsync(r => r.Username == user.Id);
            return View(await _context.Bebidas.ToListAsync());
        }

        // GET: Bebidas/Details/5
        /// <summary>
        /// mostra os detalhes das bebidas
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                //caso o id seja nulo retorna-se à página Index das Bebidas
                return RedirectToAction("Index", "Bebidas");
            }

            var bebidas = await _context.Bebidas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bebidas == null)
            {
                //caso nao seja possivel encontrar uma bebida retorna-se à página Index das bebidas
                return RedirectToAction("Index", "Bebidas");
            }

            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Clientes.FirstOrDefaultAsync(r => r.Username == user.Id);

            return View(bebidas);
        }

        // GET: Bebidas/Create
        /// <summary>
        /// Apenas o gestor cria bebidas
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Bebidas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Gestor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Preco,Imagem,Stock,CategoriaFK")] Bebidas bebida, IFormFile ImagemBeb)
        {
            // variáveis auxiliares
            string caminhoCompleto = "";
            bool hImagem = false;

            //caso nao exista imagem é atribuida uma imagem default
            if (ImagemBeb == null) { bebida.Imagem = "noDrink.png"; }
            else
            {
                //as extensões aceites são ".jpeg"; ".jpg" e ".png"
                if (ImagemBeb.ContentType == "image/jpeg" || ImagemBeb.ContentType == "image/jpg" || ImagemBeb.ContentType == "image/png")
                {
                    // o ficheiro é uma imagem válida
                    // preparar a imagem para ser guardada no disco rígido
                    Guid g;
                    g = Guid.NewGuid();
                    string extensao = Path.GetExtension(ImagemBeb.FileName).ToLower();
                    string nomebeb = g.ToString() + extensao;

                    // onde guardar o ficheiro
                    caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Imagens", nomebeb);

                    bebida.Imagem = nomebeb;

                    // assinalar que existe imagem e é preciso guardá-la no disco rígido
                    hImagem = true;
                }
                else
                {
                    //caso exista imagem, mas não tem uma das extensões exigidas, é atribuida uma imagem default
                    bebida.Imagem = "noDrink.png";
                }

            }

            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(bebida);
                    await _context.SaveChangesAsync();
                    // se há imagem, vou guardá-la no disco rígido
                    if (hImagem)
                    {
                        using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                        await ImagemBeb.CopyToAsync(stream);
                        
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);

                }

                /*_context.Add(bebida);
                await _context.SaveChangesAsync();*/
                return RedirectToAction(nameof(Index));
            }
            
            return View(bebida);
        }

        // GET: Bebidas/Edit/5
        // GET: Jogos/Edit/5
        /// <summary>
        /// edição dos dados de uma bebida por parte do gestor
        /// </summary>
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //caso o id seja nulo retorna-se à página Index das Bebidas
                return RedirectToAction("Index", "Bebidas");
            }

            var bebidas = await _context.Bebidas.FindAsync(id);
            if (bebidas == null)
            {
                //caso nao seja possivel identificar a bebida retorna-se à página Index das Bebidas
                return RedirectToAction("Index", "Bebidas");
            }
            
            return View(bebidas);
        }

        // POST: Bebidas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Preco,Imagem,Stock,CategoriaFK")] Bebidas bebidas)
        {
            if (id != bebidas.Id)
            {
                //caso o id seja nulo retorna-se à página Index das Bebidas
                return RedirectToAction("Index", "Bebidas");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    var tree = await _context.Bebidas.Include(r => r.ListaDeReservas).FirstOrDefaultAsync(b => b.Id == bebidas.Id);


                    var cliente = await _userManager.GetUserAsync(User);
                    var util = await _context.Clientes.FirstOrDefaultAsync(r => r.Username == cliente.Id);
                    
                    _context.SaveChangesAsync();

                    _context.Update(tree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BebidasExists(bebidas.Id))
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
        return View(bebidas);
    }

    // GET: Bebidas/Delete/5
    [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Bebidas");
            }

            var bebidas = await _context.Bebidas
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bebidas == null)
            {
                return RedirectToAction("Index", "Bebidas");
            }

            return View(bebidas);
        }

        // POST: Bebidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bebidas = await _context.Bebidas.FindAsync(id);
            _context.Bebidas.Remove(bebidas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BebidasExists(int id)
        {
            return _context.Bebidas.Any(e => e.Id == id);
        }
    }
}
