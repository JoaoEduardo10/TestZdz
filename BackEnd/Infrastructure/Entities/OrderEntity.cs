using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    [Table("pedidos")]
    public class OrderEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [ForeignKey(nameof(ProdutoId))]
        public ProductEntity Produtos { get; set; }

        [Required]
        public int Quantity {  get; set; }

        [Required]
        public float Valor { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public OrderEntity(ProductEntity produtos, float valor, int quantity)
        {
            Produtos = produtos;
            Valor = valor;
            ProdutoId = produtos.Id;
            CreatedAt = DateTime.UtcNow;
            Quantity = quantity;
        }

        public OrderEntity(int produtoId, float valor, int quantity)
        {
            Valor = valor;
            ProdutoId = produtoId;
            CreatedAt = DateTime.UtcNow;
            Quantity = quantity;
        }

        public OrderEntity()
        {
        }
    }
}
