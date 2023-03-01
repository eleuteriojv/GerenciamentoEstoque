using GerenciamentoEstoque.Api.Data;
using GerenciamentoEstoque.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Api.Controllers
{
    [ApiController]
    [Route("api/loja")]
    public class LojaController : Controller
    {
        private readonly GerenciamentoDbContext _context;
        public LojaController(GerenciamentoDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loja>>> Get()
        {
            return await _context.Lojas.Include(x => x.Estoques).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loja>> Get(int id)
        {
            var loja = await _context.Lojas.FindAsync(id);

            if (loja == null)
            {
                return NotFound();
            }

            return loja;
        }

        [HttpPost]
        public async Task<ActionResult<Loja>> Post(Loja loja)
        {
            _context.Lojas.Add(loja);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = loja.Id }, loja);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Loja loja)
        {
            if (id != loja.Id)
            {
                return BadRequest();
            }

            _context.Entry(loja).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LojaExists(id))
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

        private bool LojaExists(int id)
        {
            var loja = _context.Lojas.FindAsync(id);
            return true;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var loja = await _context.Lojas.FindAsync(id);

            if (loja == null)
            {
                return NotFound();
            }
            _context.Lojas.Remove(loja);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
