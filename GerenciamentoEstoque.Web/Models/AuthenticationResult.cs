using System.Text.Json.Serialization;

namespace GerenciamentoEstoque.Web.Models
{
    public class AuthenticationResult
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}