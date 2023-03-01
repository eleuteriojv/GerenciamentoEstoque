using GerenciamentoEstoque.Api.Models;
using GerenciamentoEstoque.Api.Repositories;
using GerenciamentoEstoque.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] Login login)
        {
            // Recupera o usuário
            var user = UserRepository.Get(login.UserName, login.Password);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);
            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                User = user,
                token = token,
            };
        }
    }
}
