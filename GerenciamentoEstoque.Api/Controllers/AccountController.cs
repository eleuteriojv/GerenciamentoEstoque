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
            var user = UserRepository.Get(login.UserName, login.Password);
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });
            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                User = user,
                token = token,
            };
        }
    }
}
