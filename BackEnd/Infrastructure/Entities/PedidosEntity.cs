using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class PedidosEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [ForeignKey(nameof(ProdutoId))]
        public ProdutosEntity Produtos { get; set; }

        [Required]
        public float Valor { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public PedidosEntity(ProdutosEntity produtos, float valor, DateTime createdAt)
        {
            Produtos = produtos;
            Valor = valor;
            CreatedAt = createdAt;
        }
    }
}
