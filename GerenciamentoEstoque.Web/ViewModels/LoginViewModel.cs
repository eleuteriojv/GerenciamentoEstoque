using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoEstoque.Web.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Necessário preencher o nome de usuário")]
        [DisplayName("Nome de Usuário")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Necessário preencher a senha")]
        [DisplayName("Senha")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
