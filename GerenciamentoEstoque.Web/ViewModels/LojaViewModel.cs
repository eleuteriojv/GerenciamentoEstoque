using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GerenciamentoEstoque.Web.ViewModels
{
    public class LojaViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Necessário preencher o nome da loja")]
        [DisplayName("Nome da Loja")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Necessário preencher o endereço da loja")]
        [DisplayName("Endereço da Loja")]
        public string Endereco { get; set; }
        public ICollection<ItemEstoqueViewModel> Estoques { get; set; }
    }
}
