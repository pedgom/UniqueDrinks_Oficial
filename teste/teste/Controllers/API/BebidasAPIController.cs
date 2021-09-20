using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using teste.Data;
using teste.Models;

namespace teste.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BebidasAPIController : ControllerBase
    {
        private readonly Teste _context;

        public BebidasAPIController(Teste context)
        {
            _context = context;
        }

        // GET: api/BebidasAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BebidasAPIViewModel>>> GetBebidas()
        {
            return await _context.Bebidas
                                   .Select(b => new BebidasAPIViewModel
                                   {
                                       IdBebida = b.Id,
                                       NomeBebida = b.Nome,
                                       DescricaoBebida = b.Descricao,
                                       ImagemBebida = b.Imagem,
                                       PrecoBebida = b.Preco,
                                       StockBebida = b.Stock
                                   })
                
                                .ToListAsync();
        }

        // GET: api/BebidasAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bebidas>> GetBebidas(int id)
        {
            var bebidas = await _context.Bebidas.FindAsync(id);

            if (bebidas == null)
            {
                return NotFound();
            }

            return bebidas;
        }

        // PUT: api/BebidasAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBebidas(int id, Bebidas bebidas)
        {
            if (id != bebidas.Id)
            {
                return BadRequest();
            }

            _context.Entry(bebidas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BebidasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BebidasAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bebidas>> PostBebidas(Bebidas bebidas)
        {
            _context.Bebidas.Add(bebidas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBebidas", new { id = bebidas.Id }, bebidas);
        }

        // DELETE: api/BebidasAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBebidas(int id)
        {
            var bebidas = await _context.Bebidas.FindAsync(id);
            if (bebidas == null)
            {
                return NotFound();
            }

            _context.Bebidas.Remove(bebidas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BebidasExists(int id)
        {
            return _context.Bebidas.Any(e => e.Id == id);
        }
    }
}
