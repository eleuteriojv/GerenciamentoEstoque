using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GerenciamentoEstoque.Api.Models
{
    public class ItemEstoque
    {
        [Required(ErrorMessage = "Necessário preencher quantidade de produtos no estoque")]
        [DisplayName("Quantidade")]
        public int Quantidade { get; set; }
        public int LojaId { get; set; }
        public int ProdutoId { get; set; }
        public virtual Loja Lojas { get; set; }
        public virtual Produto Produtos { get; set; }
    }
}
