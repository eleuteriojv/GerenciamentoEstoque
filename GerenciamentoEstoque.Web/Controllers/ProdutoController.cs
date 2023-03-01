using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using GerenciamentoEstoque.Web.ViewModels;
using System.Net.Http.Json;

namespace GerenciamentoEstoque.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly string endpoint = "https://localhost:44344/api/produto";
        private readonly HttpClient httpClient = null;
        public ProdutoController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(endpoint);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<ProdutoViewModel> produtos = new List<ProdutoViewModel>();
                HttpResponseMessage response = await httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    string conteudo = await response.Content.ReadAsStringAsync();
                    produtos = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(conteudo);
                }
                return View(produtos);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProdutoViewModel produtos)
        {
            try
            {
                if (produtos == null)
                {
                    return BadRequest();
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(endpoint);
                    var postTask = client.PostAsJsonAsync<ProdutoViewModel>("Produto", produtos);
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
            return View(produtos);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                ProdutoViewModel produto = null;
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(endpoint);
                    var readTask = client.GetAsync(endpoint + "/" + id);
                    readTask.Wait();
                    var result = await readTask;
                    if (result.IsSuccessStatusCode)
                    {
                        string conteudo = await result.Content.ReadAsStringAsync();
                        produto = JsonConvert.DeserializeObject<ProdutoViewModel>(conteudo);
                        return View(produto);
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
        public async Task<IActionResult> Edit(int id, ProdutoViewModel produto)
        {
            try
            {
                if (id != produto.Id)
                {
                    return BadRequest();
                }
                using (var client = new HttpClient())
                {
                    string data = JsonConvert.SerializeObject(produto);
                    StringContent conteudo = new StringContent(data, Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(endpoint);
                    HttpResponseMessage response = await client.PutAsJsonAsync(endpoint + "/" + produto.Id, conteudo);
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
            return View(produto);
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
                ProdutoViewModel produto = null;
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(endpoint);
                    var deleteTask = client.GetAsync(endpoint + "/" + id);
                    deleteTask.Wait();
                    var result = await deleteTask;
                    if (result.IsSuccessStatusCode)
                    {
                        string conteudo = await result.Content.ReadAsStringAsync();
                        produto = JsonConvert.DeserializeObject<ProdutoViewModel>(conteudo);
                        return View(produto);
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
        public async Task<IActionResult> Delete(ProdutoViewModel produto)
        {
            try
            {
                if (produto.Id == null)
                {
                    return BadRequest();
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(endpoint);
                    var deleteTask = client.DeleteAsync(endpoint + "/" + produto.Id);
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
                ProdutoViewModel produto = new ProdutoViewModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(endpoint);
                    var detailsTask = client.GetAsync(endpoint + "/" + id);
                    detailsTask.Wait();
                    var result = await detailsTask;
                    if (result.IsSuccessStatusCode)
                    {
                        string conteudo = await result.Content.ReadAsStringAsync();
                        produto = JsonConvert.DeserializeObject<ProdutoViewModel>(conteudo);
                        return View(produto);
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
