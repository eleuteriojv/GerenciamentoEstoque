using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using GerenciamentoEstoque.Web.ViewModels;
using System.Net.Http.Json;

namespace GerenciamentoEstoque.Web.Controllers
{
    public class ItemEstoqueController : Controller
    {
        private readonly string endpoint = "https://localhost:44344/api/itemestoque";
        private readonly string endpointLoja = "https://localhost:44344/api/loja";
        private readonly string endpointProduto = "https://localhost:44344/api/produto";
        private readonly HttpClient httpClient = null;
        public ItemEstoqueController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(endpoint);
            httpClient.BaseAddress = new Uri(endpointLoja);
            httpClient.BaseAddress = new Uri(endpointProduto);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<ItemEstoqueViewModel> lojas = new List<ItemEstoqueViewModel>();
                HttpResponseMessage response = await httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    string conteudo = await response.Content.ReadAsStringAsync();
                    lojas = JsonConvert.DeserializeObject<List<ItemEstoqueViewModel>>(conteudo);

                }
                return View(lojas);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                List<LojaViewModel> estoques = new List<LojaViewModel>();
                HttpResponseMessage response = await httpClient.GetAsync(endpointLoja);
                if (response.IsSuccessStatusCode)
                {
                    string conteudo = await response.Content.ReadAsStringAsync();
                    estoques = JsonConvert.DeserializeObject<List<LojaViewModel>>(conteudo);
                    ViewBag.Lojas = estoques.Select(e => new SelectListItem { Text = e.Nome, Value = e.Id.ToString() }).ToList();
                }

                List<ProdutoViewModel> produtos = new List<ProdutoViewModel>();
                HttpResponseMessage responses = await httpClient.GetAsync(endpointProduto);
                if (responses.IsSuccessStatusCode)
                {
                    string conteudo = await responses.Content.ReadAsStringAsync();
                    produtos = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(conteudo);
                    ViewBag.Produtos = produtos.Select(e => new SelectListItem { Text = e.Nome, Value = e.Id.ToString() }).ToList();
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ItemEstoqueViewModel itemEstoque)
        {
            try
            {

                if (itemEstoque == null)
                {
                    return BadRequest();
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(endpoint);
                    var postTask = client.PostAsJsonAsync<ItemEstoqueViewModel>("itemEstoque", itemEstoque);
                    postTask.Wait();
                    var result = await postTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                return NotFound();
            }
            return View(itemEstoque);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(ItemEstoqueViewModel item)
        {
            try
            {
                if (item.ProdutoId == 0 && item.LojaId == 0)
                {
                    return BadRequest();
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(endpointLoja);

                    // Obtém o produto pelo id
                    var response = await client.GetAsync("?id=" + item.ProdutoId.ToString() + "?id=" + item.LojaId);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var loja = JsonConvert.DeserializeObject<LojaViewModel[]>(json);
                        var lojas = loja.Where(x => x.Id == item.LojaId).FirstOrDefault();
                        return View(lojas);
                    }
                    else
                    {
                        ModelState.AddModelError(null, "Erro ao processar a solicitação");
                    }
                }

                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ItemEstoqueViewModel item)
        {
            try
            {
                if (item.ProdutoId == 0 && item.LojaId == 0)
                {
                    return BadRequest();
                }
                using (var client = new HttpClient())
                {
                    string data = JsonConvert.SerializeObject(item);
                    StringContent conteudo = new StringContent(data, Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(endpointLoja);
                    HttpResponseMessage response = await client.PostAsync(client.BaseAddress + "/" + item.ProdutoId + "/" + item.LojaId, conteudo);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                LojaViewModel loja = null;
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(endpointLoja);
                    var deleteTask = client.GetAsync(endpointLoja + "/" + id);
                    deleteTask.Wait();
                    var result = await deleteTask;
                    if (result.IsSuccessStatusCode)
                    {
                        string conteudo = await result.Content.ReadAsStringAsync();
                        loja = JsonConvert.DeserializeObject<LojaViewModel>(conteudo);
                        return View(loja);
                    }
                }
            }
            catch
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(LojaViewModel loja)
        {
            try
            {
                if (loja.Id == null)
                {
                    return BadRequest();
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(endpointLoja);
                    var deleteTask = client.DeleteAsync(endpointLoja + "/" + loja.Id);
                    deleteTask.Wait();
                    var result = await deleteTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                return NotFound();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                LojaViewModel loja = new LojaViewModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(endpointLoja);
                    var detailsTask = client.GetAsync(endpointLoja + "/" + id);
                    detailsTask.Wait();
                    var result = await detailsTask;
                    if (result.IsSuccessStatusCode)
                    {
                        string conteudo = await result.Content.ReadAsStringAsync();
                        loja = JsonConvert.DeserializeObject<LojaViewModel>(conteudo);
                        return View(loja);
                    }

                }
            }
            catch
            {
                return NotFound();
            }
            return View();
        }
    }
}
