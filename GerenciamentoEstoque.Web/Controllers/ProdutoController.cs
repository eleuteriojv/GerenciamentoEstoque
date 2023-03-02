using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using GerenciamentoEstoque.Web.ViewModels;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using GerenciamentoEstoque.Web.Services.Interfaces;
using System.Net.Http.Headers;

namespace GerenciamentoEstoque.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly string endpoint = "https://localhost:44344/api/produto";
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        public ProdutoController(ITokenService tokenService)
        {
            _httpClient = new HttpClient();
            _tokenService = tokenService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<ProdutoViewModel> produtos = new List<ProdutoViewModel>();
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    string conteudo = await response.Content.ReadAsStringAsync();
                    produtos = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(conteudo);
                }
                return View(produtos);
            }
            catch
            {
                return StatusCode(401, "Você não está autorizado");

            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            var authToken = _tokenService.GetTokenFromRequest(Request);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
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
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                _httpClient.BaseAddress = new Uri(endpoint);
                var result = await _httpClient.PostAsJsonAsync<ProdutoViewModel>("Produto", produtos);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return StatusCode(401, "Você não está autorizado");
            }
            return View(produtos);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                ProdutoViewModel produto = null;
                _httpClient.BaseAddress = new Uri(endpoint);
                var result = await _httpClient.GetAsync(endpoint + "/" + id);
                if (result.IsSuccessStatusCode)
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<ProdutoViewModel>(conteudo);
                    return View(produto);
                }
            }
            catch
            {
                return StatusCode(401, "Você não está autorizado");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProdutoViewModel produto)
        {
            try
            {
                if (produto == null)
                {
                    return BadRequest();
                }
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                _httpClient.BaseAddress = new Uri(endpoint);
                var result = await _httpClient.PutAsJsonAsync<ProdutoViewModel>(endpoint + "/" + produto.Id, produto);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return StatusCode(401, "Você não está autorizado");
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
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                _httpClient.BaseAddress = new Uri(endpoint);
                var result = await _httpClient.GetAsync(endpoint + "/" + id);
                if (result.IsSuccessStatusCode)
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<ProdutoViewModel>(conteudo);
                    return View(produto);
                }
            }
            catch
            {
                return StatusCode(401, "Você não está autorizado");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ProdutoViewModel produto)
        {
            try
            {
                if (produto.Id == 0)
                {
                    return BadRequest();
                }
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                _httpClient.BaseAddress = new Uri(endpoint);
                var result = await _httpClient.DeleteAsync(endpoint + "/" + produto.Id);
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
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                _httpClient.BaseAddress = new Uri(endpoint);
                var result = await _httpClient.GetAsync(endpoint + "/" + id);
                if (result.IsSuccessStatusCode)
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<ProdutoViewModel>(conteudo);
                    return View(produto);
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
