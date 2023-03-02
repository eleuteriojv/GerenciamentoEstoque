using GerenciamentoEstoque.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GerenciamentoEstoque.Web.Services
{
    public class TokenService : ITokenService
    {
        public string GetTokenFromRequest(HttpRequest httpRequest)
        {
            if (httpRequest.Cookies.TryGetValue("token", out var authToken))
                return authToken;
            return "";
        }
    }
}