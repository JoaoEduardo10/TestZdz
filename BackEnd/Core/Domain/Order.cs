using Core.Exceptions.enums;
using Core.Exceptions;

namespace Core.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public float Valor { get; set; }

        public DateTime CreatedAt { get; set; }

        public Order(int id, Product product, float valor, DateTime createdAt, int quantity)
        {
            Id = id;
            Product = product;
            Valor = MinValue(valor);
            CreatedAt = createdAt;
            Quantity = quantity;
        }

        public Order(Product product, float valor, int quantity)
        {
            Product = product;
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
                throw new OrderException(ErrorCodeEnum.PRO0002.GetMessage(), ErrorCodeEnum.PRO0002.GetCode());
            }

            return value;
        }
    }
}