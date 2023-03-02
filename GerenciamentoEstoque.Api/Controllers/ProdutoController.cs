using GerenciamentoEstoque.Api.Data;
using GerenciamentoEstoque.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : Controller
    {
        private readonly GerenciamentoDbContext _context;
        public ProdutoController(GerenciamentoDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            try
            {
                return await _context.Produtos.Include(x => x.Estoques).ToListAsync();
            }
            catch (Exception)
            {
                return StatusCode(401, "Você não está autorizado");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            try
            {
                var produto = await _context.Produtos.FindAsync(id);

                if (produto == null)
                {
                    return NotFound();
                }
                return produto;
            }
            catch (Exception)
            {
                return StatusCode(401, "Você não está autorizado");
            }
            
        }
        [HttpPost]
        public async Task<ActionResult<Produto>> Post(Produto produto)
        {
            try
            {
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao cadastrar produtos.");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }
            _context.Entry(produto).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
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

        private bool ProdutoExists(int id)
        {
            var produto = _context.Produtos.FindAsync(id);
            return true;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
