using Core.Exceptions.enums;
using Core.Exceptions;

namespace Core.Domain
{
    public class Pedidos
    {
        public int Id { get; set; }
        public Produtos Produto { get; set; }
        public float Valor { get; set; }

        public Pedidos(int id, Produtos produto, float valor)
        {
            Id = id;
            Produto = produto;
            Valor = MinValue(valor);
        }

        public Pedidos(Produtos produto, float valor)
        {
            Produto = produto;
            Valor = MinValue(valor);
        }

        public Pedidos()
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