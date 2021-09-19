using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using teste.Data;
using teste.Models;

namespace teste.Controllers
{
    [Authorize]
    public class ReservasController : Controller
    {
        private readonly Teste _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservasController(Teste context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Clientes.FirstOrDefaultAsync(r => r.Username == user.Id);
            var teste = _context.Reservas.Include(r => r.Bebida).Include(r => r.Cliente).Where(r => r.ClienteFK == util.Id);
            return View(await teste.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas
                .Include(r => r.Bebida)
                .Include(r => r.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservas == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Clientes.FirstOrDefaultAsync(r => r.Username == user.Id);

            return View(reservas);
        }

        // GET: Reservas/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["BebidaFK"] = new SelectList(_context.Set<Bebidas>(), "Id", "Id");
            ViewData["ClienteFK"] = new SelectList(_context.Set<Clientes>(), "Id", "Email");

            var user = await _userManager.GetUserAsync(User);
            var util = await _context.Clientes.FirstOrDefaultAsync(r => r.Username == user.Id);
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataEntrega,Estado,Quantidade,BebidaFK,ClienteFK")] Reservas reservas)
        {
            if (ModelState.IsValid)
            {
                reservas.DataReserva = DateTime.Now;
                var cliente = await _userManager.GetUserAsync(User);
                var util = await _context.Clientes.FirstOrDefaultAsync(r => r.Username == cliente.Id);
                reservas.ClienteFK = util.Id;
                _context.Update(util);
                _context.Add(reservas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BebidaFK"] = new SelectList(_context.Set<Bebidas>(), "Id", "Id", reservas.BebidaFK);
            ViewData["ClienteFK"] = new SelectList(_context.Set<Clientes>(), "Id", "Email", reservas.ClienteFK);
            return View(reservas);
        }

        // GET: Reservas/Edit/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas.FindAsync(id);
            if (reservas == null)
            {
                return NotFound();
            }
            ViewData["BebidaFK"] = new SelectList(_context.Set<Bebidas>(), "Id", "Id", reservas.BebidaFK);
            ViewData["ClienteFK"] = new SelectList(_context.Set<Clientes>(), "Id", "Email", reservas.ClienteFK);
            return View(reservas);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataReserva,DataEntrega,Estado,Quantidade,BebidaFK,ClienteFK")] Reservas reservas)
        {
            if (id != reservas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservasExists(reservas.Id))
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
            ViewData["BebidaFK"] = new SelectList(_context.Set<Bebidas>(), "Id", "Id", reservas.BebidaFK);
            ViewData["ClienteFK"] = new SelectList(_context.Set<Clientes>(), "Id", "Email", reservas.ClienteFK);
            return View(reservas);
        }

        // GET: Reservas/Delete/5
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas
                .Include(r => r.Bebida)
                .Include(r => r.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservas == null)
            {
                return NotFound();
            }

            return View(reservas);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservas = await _context.Reservas.FindAsync(id);
            _context.Reservas.Remove(reservas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservasExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
