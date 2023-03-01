using GerenciamentoEstoque.Api.Models;
using GerenciamentoEstoque.Api.Repositories;
using GerenciamentoEstoque.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Api.Controllers
{
    [Route("v1/account")]
    public class AccountController : ControllerBase
    {

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] Login model)
        {
            // Recupera o usuário
            var user = UserRepository.Get(model.UserName, model.Password);

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
                user = user,
                token = token
            };
        }
    }
}
