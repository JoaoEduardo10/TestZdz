using Core.Domain;
using Infrastructure.Dtos.Request;
using Infrastructure.Entities;

namespace Infrastructure.Mapper
{
    public class OrderMapper
    {
        private readonly ProdutsMapper _ProdutsMapper;

        public OrderMapper(ProdutsMapper produtsMapper)
        {
            _ProdutsMapper = produtsMapper;
        }

        public OrderEntity ToOrderEntity(Order order, ProdutosEntity produtos)
        {
            return new OrderEntity(
                produtos,
                order.Valor,
                order.Quantity
            );
        }


        public Order ToRequest(OrderRequestDto orderRequestDto)
        {
            return new Order(
               _ProdutsMapper.ToProdut(orderRequestDto.ProdutoId),
               orderRequestDto.Value,
               orderRequestDto.Quantity
             );
        }
    }
}
