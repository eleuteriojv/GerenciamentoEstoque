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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
                    ViewBag.Produtos = produtos.Where(x => x.Estoques.Count == 0).Select(e => new SelectListItem { Text = e.Nome, Value = e.Id.ToString() }).ToList();
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
        public async Task<IActionResult> Edit(int idProduto, int idLoja)
        {
            try
            {
                if (idProduto == 0 && idLoja == 0)
                {
                    return BadRequest();
                }

                ItemEstoqueViewModel item = null;
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(endpoint);
                    var readTask = client.GetAsync(endpoint + "/" + idProduto + "/" + idLoja);
                    readTask.Wait();
                    var result = await readTask;
                    if (result.IsSuccessStatusCode)
                    {
                        string conteudo = await result.Content.ReadAsStringAsync();
                        item = JsonConvert.DeserializeObject<ItemEstoqueViewModel>(conteudo);
                        List<SelectListItem> produto = new List<SelectListItem>
                        {
                            new SelectListItem
                            {
                                Text = item.Produtos.Nome, 
                                Value = item.Produtos.Id.ToString()
                            }
                        };
                        List<SelectListItem> loja = new List<SelectListItem>
                        {
                            new SelectListItem
                            {
                                Text = item.Lojas.Nome, 
                                Value = item.Lojas.Id.ToString()
                            }
                        };
                        ViewBag.Loja = loja;
                        ViewBag.Produto = produto;
                        return View(item);
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
                    client.BaseAddress = new Uri(endpoint);
                    var putTask = client.PutAsJsonAsync<ItemEstoqueViewModel>(endpoint + "/" + item.ProdutoId + item.LojaId, item);
                    putTask.Wait();
                    var result = await putTask;
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
            return View(item);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int idProduto, int idLoja)
        {
            try
            {
                if (idProduto == 0 && idLoja == 0)
                {
                    return BadRequest();
                }

                ItemEstoqueViewModel item = null;
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(endpoint);
                    var readTask = client.GetAsync(endpoint + "/" + idProduto + "/" + idLoja);
                    readTask.Wait();
                    var result = await readTask;
                    if (result.IsSuccessStatusCode)
                    {
                        string conteudo = await result.Content.ReadAsStringAsync();
                        item = JsonConvert.DeserializeObject<ItemEstoqueViewModel>(conteudo);
                        return View(item);
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
        public async Task<IActionResult> Delete(ItemEstoqueViewModel item)
        {
            try
            {
                if (item.ProdutoId == 0 && item.LojaId == 0)
                {
                    return BadRequest();
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(endpoint);
                    var deleteTask = client.DeleteAsync(endpoint + "/" + item.ProdutoId + "/" + item.LojaId);
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
        
    }
}
