using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using GerenciamentoEstoque.Web.ViewModels;
using System.Linq;
using System.Net.Http.Json;

namespace GerenciamentoEstoque.Web.Controllers
{
    public class LojaController : Controller
    {
        private readonly string endpointLoja = "https://localhost:44344/api/loja";
        private readonly string endpointEstoque = "https://localhost:44344/api/itemEstoque/api/itemLoja";
        private readonly HttpClient httpClient = null;
        public LojaController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(endpointLoja);
            httpClient.BaseAddress = new Uri(endpointEstoque);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<LojaViewModel> lojas = new List<LojaViewModel>();
                HttpResponseMessage response = await httpClient.GetAsync(endpointLoja);
                if (response.IsSuccessStatusCode)
                {
                    string conteudo = await response.Content.ReadAsStringAsync();
                    lojas = JsonConvert.DeserializeObject<List<LojaViewModel>>(conteudo);
                }
                return View(lojas);
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
        public async Task<IActionResult> Create(LojaViewModel lojas)
        {
            try
            {
                if (lojas == null)
                {
                    return BadRequest();
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(endpointLoja);
                    var postTask = client.PostAsJsonAsync<LojaViewModel>("Loja", lojas);
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
            return View(lojas);
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
                LojaViewModel loja = null;
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(endpointLoja);
                    var readTask = client.GetAsync(endpointLoja + "/" + id);
                    readTask.Wait();
                    var result = await readTask;
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
        public async Task<IActionResult> Edit(int id, LojaViewModel loja)
        {
            try
            {
                if (loja == null)
                {
                    return BadRequest();
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(endpointLoja);
                    var postTask = client.PutAsJsonAsync<LojaViewModel>(endpointLoja + "/" + loja.Id, loja);
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
            return View(loja);
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
        [HttpGet]
        public async Task<IActionResult> ItemLoja(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();

                }
                List<ItemEstoqueViewModel> estoque = new List<ItemEstoqueViewModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(endpointEstoque);
                    var itemLoja = client.GetAsync(endpointEstoque + "/" + id);
                    itemLoja.Wait();
                    var result = await itemLoja;
                    if (result.IsSuccessStatusCode)
                    {
                        string conteudo = await result.Content.ReadAsStringAsync();
                        estoque = JsonConvert.DeserializeObject<List<ItemEstoqueViewModel>>(conteudo);
                        return View(estoque);
                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
            return View();
        }
    }
}