namespace Infrastructure.Dtos.Request
{
    public class OrderRequestDto
    {
        public int ProdutoId { get; set; }
        public int Quantity {  get; set; }

        public OrderRequestDto(int produtoId, int quantity)
        {
            ProdutoId = produtoId;
            Quantity = quantity;
        }
    }
}
