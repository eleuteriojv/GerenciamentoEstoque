using GerenciamentoEstoque.Web.Models;
using GerenciamentoEstoque.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                    return BadRequest();
                using var client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                var result = await client.PostAsJsonAsync("Login", login);
                if (result.IsSuccessStatusCode)
                {
                    var loginResult = await result.Content.ReadFromJsonAsync<AuthenticationResult>();
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddMonths(1),
                        Path = "/"
                    };
                    Response.Cookies.Append("token", loginResult.Token, cookieOptions);
                    return RedirectToAction("Index", "Home");
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
