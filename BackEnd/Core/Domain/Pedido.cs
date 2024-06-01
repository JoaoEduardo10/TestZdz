namespace Core.Domain
{
    public class Pedido
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public float Valor { get; set; }
    }
}