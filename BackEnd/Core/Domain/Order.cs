using Core.Exceptions.enums;
using Core.Exceptions;

namespace Core.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public Produtos Produto { get; set; }
        public int Quantity { get; set; }
        public float Valor { get; set; }

        public DateTime CreatedAt { get; set; }

        public Order(int id, Produtos produto, float valor, DateTime createdAt, int quantity)
        {
            Id = id;
            Produto = produto;
            Valor = MinValue(valor);
            CreatedAt = createdAt;
            Quantity = quantity;
        }

        public Order(Produtos produto, float valor, int quantity)
        {
            Produto = produto;
            Valor = MinValue(valor);
            Quantity = quantity;
            CreatedAt = DateTime.UtcNow;
        }

        public Order(float valor, int quantity)
        {
            Valor = valor;
            Quantity = quantity;
            CreatedAt = DateTime.UtcNow;
        }

        public Order()
        {
        }

        private static float MinValue(float value)
        {
            if (value <= 0)
            {
                throw new ProdutoException(ErrorCodeEnum.PRO0002.GetMessage(), ErrorCodeEnum.PRO0002.GetCode());
            }

            return value;
        }
    }
}