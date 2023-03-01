using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using GerenciamentoEstoque.Api.Models;
using GerenciamentoEstoque.Api.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace GerenciamentoEstoque.Api.Controllers
{
    [ApiController]
    [Route("api/itemestoque")]
    public class ItemEstoqueController : Controller
    {
        private readonly GerenciamentoDbContext _context;
        public ItemEstoqueController(GerenciamentoDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemEstoque>>> Get()
        {
            try
            {
                var itemEstoque = _context.ItemEstoques.Include(x => x.Lojas).Include(x => x.Produtos).AsNoTracking().AsAsyncEnumerable();
                return Ok(itemEstoque);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao buscar os livros.");
            }
        }

        [HttpGet("{idProduto}/{idLoja}")]
        public async Task<ActionResult<ItemEstoque>> Get(int idProduto, int idLoja)
        {
            var itemEstoque = await _context.ItemEstoques
                .Include(x => x.Produtos)
                .Include(x => x.Lojas)
                .FirstOrDefaultAsync(x => x.ProdutoId == idProduto && x.LojaId == idLoja);

            if (itemEstoque == null)
            {
                return NotFound();
            }

            return itemEstoque;
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post(ItemEstoque itemEstoque)
        {
            _context.ItemEstoques.Add(itemEstoque);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), itemEstoque);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ItemEstoque itemEstoque)
        {
            if (itemEstoque.ProdutoId == 0)
            {
                return BadRequest();
            }
            if (itemEstoque.LojaId == 0)
            {
                return BadRequest();
            }

            _context.Entry(itemEstoque).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao buscar os livros.");
            }

            return NoContent();
        }
        [HttpDelete("{idProduto}/{idLoja}")]
        public async Task<IActionResult> Delete(int idProduto, int idLoja)
        {
            var itemEstoque = await _context.ItemEstoques
                .Include(x => x.Produtos)
                .Include(x => x.Lojas)
                .FirstOrDefaultAsync(x => x.ProdutoId == idProduto && x.LojaId == idLoja);

            if (itemEstoque == null)
            {
                return NotFound();
            }
            _context.ItemEstoques.Remove(itemEstoque);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("api/itemloja/{id}")]
        public async Task<IActionResult> GetItemLoja(int id)
        {
            try
            {
                var item = _context.ItemEstoques.Include(x => x.Lojas).Include(x => x.Produtos).Where(y => y.LojaId == id).ToList();
                return Ok(item);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
