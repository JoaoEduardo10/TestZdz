using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    [Table("pedidos")]
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
        public int Quantity {  get; set; }

        [Required]
        public float Valor { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public PedidosEntity(ProdutosEntity produtos, float valor, int quantity)
        {
            Produtos = produtos;
            Valor = valor;
            ProdutoId = produtos.Id;
            CreatedAt = DateTime.UtcNow;
            Quantity = quantity;
        }

        public PedidosEntity(int produtoId, float valor, int quantity)
        {
            Valor = valor;
            ProdutoId = produtoId;
            CreatedAt = DateTime.UtcNow;
            Quantity = quantity;
        }

        public PedidosEntity()
        {
        }
    }
}
