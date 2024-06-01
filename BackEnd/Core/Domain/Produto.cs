using Core.Exceptions;
using Core.Exceptions.enums;

namespace Core.Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }


        public Produto(int id, string name, float value)
        {
            Id = id;
            Name = MinNameLength(name);
            Value = MinValue(value);
        }

        public Produto(string name, float value)
        {
            Name = MinNameLength(name);
            Value = MinValue(value);
        }

        public Produto()
        {
        }

        private static string MinNameLength(string name)
        {
            if (name.Length < 3)
            {
                throw new ProdutoException(ErrorCodeEnum.PRO001.GetMessage(), ErrorCodeEnum.PRO001.GetCode());
            }

            return name;
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