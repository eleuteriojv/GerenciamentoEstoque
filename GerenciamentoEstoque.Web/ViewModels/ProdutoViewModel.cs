using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GerenciamentoEstoque.Web.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Necessário preencher o nome do produto")]
        [DisplayName("Nome do Produto")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Necessário preencher o preço do produto")]
        [DisplayName("Preço do Produto")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Preco { get; set; }
        public ICollection<ItemEstoqueViewModel> Estoques { get; set; }
    }
}
