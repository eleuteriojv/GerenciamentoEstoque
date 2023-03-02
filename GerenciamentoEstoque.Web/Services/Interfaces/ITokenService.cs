using Microsoft.AspNetCore.Http;

namespace GerenciamentoEstoque.Web.Services.Interfaces
{
    public interface ITokenService
    {
        public string GetTokenFromRequest(HttpRequest httpRequest);
    }
}