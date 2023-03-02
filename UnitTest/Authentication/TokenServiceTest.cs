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

        [Fact]
        public void DadoRecebimentoDeDoisValores_DeveRetornarSoma()
        {
            var a = 10;
            var b = 15;

            var resultado = Soma(a, b);

            Assert.Equal(25, resultado);
        }

        public int Soma(int a, int b) => a + b;
    }
}