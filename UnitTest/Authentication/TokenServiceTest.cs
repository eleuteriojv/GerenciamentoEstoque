using GerenciamentoEstoque.Api.Models;
using GerenciamentoEstoque.Api.Services;
using Xunit;

namespace UnitTest.Authentication
{
    public class TokenServiceTest
    {
        [Fact]
        public void DadoRecebimentoDeLogin_DeveRetornarUmTokenDeAutenticacao()
        {
            var loginRequest = new Login()
            {
                UserName = "admin",
                Password = "admin",
                Role = "admin"
            };
            var token = TokenService.GenerateToken(loginRequest);

            Assert.NotNull(token);
        }
    }
}