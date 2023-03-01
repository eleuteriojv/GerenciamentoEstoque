using GerenciamentoEstoque.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly string endpoint = "https://localhost:44344/api/account/login";
        private readonly HttpClient httpClient = null;
        public AccountController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(endpoint);
        }
        [HttpGet]
        public IActionResult Authenticate()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate(LoginViewModel login)
        {
            try
            {
                if (login == null)
                {
                    return BadRequest();
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(endpoint);
                    var postTask = client.PostAsJsonAsync<LoginViewModel>("Login", login);
                    postTask.Wait();
                    var result = await postTask;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch
            {
                return NotFound();
            }
            return View(login);
        }
    }
}
