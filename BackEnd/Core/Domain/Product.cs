using Core.Exceptions;
using Core.Exceptions.enums;

namespace Core.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }


        public Product(int id, string name, float value)
        {
            Id = id;
            Name = MinNameLength(name);
            Value = MinValue(value);
        }

        public Product(string name, float value)
        {
            Name = MinNameLength(name);
            Value = MinValue(value);
        }

        public Product(int id)
        {
            Id = id;
        }

        public Product()
        {
        }

        private static string MinNameLength(string name)
        {
            if (name.Length < 3)
            {
                throw new ProductException(ErrorCodeEnum.PRO0001.GetMessage(), ErrorCodeEnum.PRO0001.GetCode());
            }

            return name;
        }

        private static float MinValue(float value)
        {
            if (value <= 0)
            {
                throw new ProductException(ErrorCodeEnum.PRO0002.GetMessage(), ErrorCodeEnum.PRO0002.GetCode());
            }

            return value;
        }

    }
}