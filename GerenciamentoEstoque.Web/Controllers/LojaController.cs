using GerenciamentoEstoque.Web.Services.Interfaces;
using GerenciamentoEstoque.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Web.Controllers
{
    public class LojaController : Controller
    {
        private readonly string _endpointLoja = "https://localhost:44344/api/loja";
        private readonly string _endpointEstoque = "https://localhost:44344/api/itemEstoque/itemLoja";
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        public LojaController(ITokenService tokenService)
        {
            _httpClient = new HttpClient();
            _tokenService = tokenService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<LojaViewModel> lojas = new List<LojaViewModel>();
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                HttpResponseMessage response = await _httpClient.GetAsync(_endpointLoja);
                if (response.IsSuccessStatusCode)
                {
                    string conteudo = await response.Content.ReadAsStringAsync();
                    lojas = JsonConvert.DeserializeObject<List<LojaViewModel>>(conteudo);
                }
                return View(lojas);
            }
            catch
            {
                return StatusCode(401, "Você não está autorizado");
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
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                _httpClient.BaseAddress = new Uri(_endpointLoja);
                var result = await _httpClient.PostAsJsonAsync<LojaViewModel>("Loja", lojas);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return StatusCode(401, "Você não está autorizado");
            }
            return View(lojas);
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
                LojaViewModel loja = null;
                _httpClient.BaseAddress = new Uri(_endpointLoja);
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                var result = await _httpClient.GetAsync(_endpointLoja + "/" + id);
                if (result.IsSuccessStatusCode)
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    loja = JsonConvert.DeserializeObject<LojaViewModel>(conteudo);
                    return View(loja);
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
                _httpClient.BaseAddress = new Uri(_endpointLoja);
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                var result = await _httpClient.PutAsJsonAsync<LojaViewModel>(_endpointLoja + "/" + loja.Id, loja);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
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
                _httpClient.BaseAddress = new Uri(_endpointLoja);
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                var result = await _httpClient.GetAsync(_endpointLoja + "/" + id);
                if (result.IsSuccessStatusCode)
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    loja = JsonConvert.DeserializeObject<LojaViewModel>(conteudo);
                    return View(loja);
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
                if (loja.Id == 0)
                {
                    return BadRequest();
                }
                _httpClient.BaseAddress = new Uri(_endpointLoja);
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                var result = await _httpClient.DeleteAsync(_endpointLoja + "/" + loja.Id);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return NotFound();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                LojaViewModel loja = new LojaViewModel();
                _httpClient.BaseAddress = new Uri(_endpointLoja);
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                var result = await _httpClient.GetAsync(_endpointLoja + "/" + id);
                if (result.IsSuccessStatusCode)
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    loja = JsonConvert.DeserializeObject<LojaViewModel>(conteudo);
                    return View(loja);
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
                if (id == 0)
                {
                    return BadRequest();

                }
                List<ItemEstoqueViewModel> estoque = new List<ItemEstoqueViewModel>();
                _httpClient.BaseAddress = new Uri(_endpointEstoque);
                var authToken = _tokenService.GetTokenFromRequest(Request);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                var result = await _httpClient.GetAsync(_endpointEstoque + "/" + id);
                if (result.IsSuccessStatusCode)
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    estoque = JsonConvert.DeserializeObject<List<ItemEstoqueViewModel>>(conteudo);
                    return View(estoque);
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