using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GerenciamentoEstoque.Api.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Necessário preencher o nome do produto")]
        [DisplayName("Nome do Produto")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Necessário preencher o preço do produto")]
        [DisplayName("Preço do Produto")]
        public decimal Preco { get; set; }
        public ICollection<ItemEstoque> Estoques { get; set; }
    }
}
