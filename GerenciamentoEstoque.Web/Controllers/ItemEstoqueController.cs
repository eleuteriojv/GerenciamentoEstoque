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
using GerenciamentoEstoque.Web.Services.Interfaces;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.Intrinsics.X86;

namespace GerenciamentoEstoque.Web.Controllers
{
    public class ItemEstoqueController : Controller
    {
        private readonly string _endpoint = "https://localhost:44344/api/itemestoque";
        private readonly string _endpointLoja = "https://localhost:44344/api/loja";
        private readonly string _endpointProduto = "https://localhost:44344/api/produto";
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        public ItemEstoqueController(ITokenService tokenService)
        {
            _httpClient = new HttpClient();
            _tokenService = tokenService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<ItemEstoqueViewModel> lojas = new List<ItemEstoqueViewModel>();
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                HttpResponseMessage response = await _httpClient.GetAsync(_endpoint);
                if (response.IsSuccessStatusCode)
                {
                    string conteudo = await response.Content.ReadAsStringAsync();
                    lojas = JsonConvert.DeserializeObject<List<ItemEstoqueViewModel>>(conteudo);

                }
                return View(lojas);
            }
            catch (Exception)
            {
                return StatusCode(401, "Você não está autorizado");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                List<LojaViewModel> estoques = new List<LojaViewModel>();
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                HttpResponseMessage response = await _httpClient.GetAsync(_endpointLoja);
                if (response.IsSuccessStatusCode)
                {
                    string conteudo = await response.Content.ReadAsStringAsync();
                    estoques = JsonConvert.DeserializeObject<List<LojaViewModel>>(conteudo);
                    ViewBag.Lojas = estoques.Select(e => new SelectListItem { Text = e.Nome, Value = e.Id.ToString() }).ToList();
                }

                List<ProdutoViewModel> produtos = new List<ProdutoViewModel>();
                HttpResponseMessage responses = await _httpClient.GetAsync(_endpointProduto);
                if (responses.IsSuccessStatusCode)
                {
                    string conteudo = await responses.Content.ReadAsStringAsync();
                    produtos = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(conteudo);
                    ViewBag.Produtos = produtos.Select(e => new SelectListItem { Text = e.Nome, Value = e.Id.ToString() }).ToList();
                }
            }
            catch (Exception)
            {
                return StatusCode(401, "Você não está autorizado");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ItemEstoqueViewModel itemEstoque)
        {
            try
            {
                var verificaItem = VerificaItemEstoque(itemEstoque);

                if (itemEstoque == null)
                {
                    return BadRequest();
                }
                ViewBag.Lojas = await GetLojas();
                ViewBag.Produtos = await GetProdutos();
                if (verificaItem.Result)
                {
                    TempData["Msg"] = "A loja já possui esse produto no estoque, não é necessário cadastrar.";
                    return View(itemEstoque);
                }
                _httpClient.BaseAddress = new Uri(_endpoint);
                var result = await _httpClient.PostAsJsonAsync<ItemEstoqueViewModel>("itemEstoque", itemEstoque);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return NotFound();
            }
            return View(itemEstoque);
        }
        private async Task<IList<SelectListItem>> GetLojas()
        {
            List<LojaViewModel> estoques = new List<LojaViewModel>();
            var authToken = _tokenService.GetTokenFromRequest(Request);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            HttpResponseMessage response = await _httpClient.GetAsync(_endpointLoja);
            if (response.IsSuccessStatusCode)
            {
                string conteudo = await response.Content.ReadAsStringAsync();
                estoques = JsonConvert.DeserializeObject<List<LojaViewModel>>(conteudo);
                var select = estoques.Select(e => new SelectListItem { Text = e.Nome, Value = e.Id.ToString() }).ToList();
                return select;
            }
            return null;
        }
        private async Task<IList<SelectListItem>> GetProdutos()
        {
            List<ProdutoViewModel> produtos = new List<ProdutoViewModel>();
            var authToken = _tokenService.GetTokenFromRequest(Request);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            HttpResponseMessage responses = await _httpClient.GetAsync(_endpointProduto);
            if (responses.IsSuccessStatusCode)
            {
                string conteudo = await responses.Content.ReadAsStringAsync();
                produtos = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(conteudo);
                var select = produtos.Select(e => new SelectListItem { Text = e.Nome, Value = e.Id.ToString() }).ToList();
                return select;
            }
            return null;
        }
        public async Task<bool> VerificaItemEstoque(ItemEstoqueViewModel itemEstoque)
        {
            var authToken = _tokenService.GetTokenFromRequest(Request);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            _httpClient.BaseAddress = new Uri(_endpoint);
            var result = await _httpClient.GetAsync(_endpoint + "/" + itemEstoque.ProdutoId + "/" + itemEstoque.LojaId);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
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
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                _httpClient.BaseAddress = new Uri(_endpoint);
                var result = await _httpClient.GetAsync(_endpoint + "/" + idProduto + "/" + idLoja);
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
            catch
            {
                return StatusCode(401, "Você não está autorizado");
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
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                _httpClient.BaseAddress = new Uri(_endpoint);
                var result = await _httpClient.PutAsJsonAsync<ItemEstoqueViewModel>(_endpoint + "/" + item.ProdutoId + item.LojaId, item);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return StatusCode(401, "Você não está autorizado");
            }
            return View(item);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int idProduto, int idLoja)
        {
            try
            {
                if (idProduto == 0 && idLoja == 0)
                {
                    return BadRequest();
                }
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                ItemEstoqueViewModel item = null;
                _httpClient.BaseAddress = new Uri(_endpoint);
                var result = await _httpClient.GetAsync(_endpoint + "/" + idProduto + "/" + idLoja);
                if (result.IsSuccessStatusCode)
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<ItemEstoqueViewModel>(conteudo);
                    return View(item);
                }
            }
            catch
            {
                return StatusCode(401, "Você não está autorizado");
            }
            return View();
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
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                ItemEstoqueViewModel item = null;
                _httpClient.BaseAddress = new Uri(_endpoint);
                var result = await _httpClient.GetAsync(_endpoint + "/" + idProduto + "/" + idLoja);
                if (result.IsSuccessStatusCode)
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<ItemEstoqueViewModel>(conteudo);
                    return View(item);
                }
            }
            catch
            {
                return StatusCode(401, "Você não está autorizado");
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
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                _httpClient.BaseAddress = new Uri(_endpoint);
                var result = await _httpClient.DeleteAsync(_endpoint + "/" + item.ProdutoId + "/" + item.LojaId);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return StatusCode(401, "Você não está autorizado");
            }
            return View();
        }

    }
}
