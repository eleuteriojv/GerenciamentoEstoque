using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GerenciamentoEstoque.Web.ViewModels
{
    public class ItemEstoqueViewModel
    {
        [Required(ErrorMessage = "Necessário preencher quantidade de produtos no estoque")]
        [DisplayName("Quantidade")]
        public int Quantidade { get; set; }
        public int LojaId { get; set; }
        public int ProdutoId { get; set; }
        public LojaViewModel Lojas { get; set; }
        public ProdutoViewModel Produtos { get; set; }
    }
}
