namespace Infrastructure.Dtos.Request
{
    public class ProductRequestDto
    {
        public string Name {  get; set; }
        public float Value { get; set; }

        public ProductRequestDto(string name, float value)
        {
            Name = name;
            Value = value;
        }
    }

    
}
