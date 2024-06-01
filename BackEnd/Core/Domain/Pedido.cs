using Core.Exceptions.enums;
using Core.Exceptions;

namespace Core.Domain
{
    public class Pedido
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public float Valor { get; set; }

        public Pedido(int id, Produto produto, float valor)
        {
            Id = id;
            Produto = produto;
            Valor = MinValue(valor);
        }

        public Pedido(Produto produto, float valor)
        {
            Produto = produto;
            Valor = MinValue(valor);
        }

        public Pedido()
        {
        }

        private static float MinValue(float value)
        {
            if (value <= 0)
            {
                throw new ProdutoException(ErrorCodeEnum.PRO002.GetMessage(), ErrorCodeEnum.PRO002.GetCode());
            }

            return value;
        }
    }
}