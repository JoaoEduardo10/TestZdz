using Core.Domain;
using Infrastructure.Dtos.Request;
using Infrastructure.Entities;

namespace Infrastructure.Mapper
{
    public class OrderMapper
    {
        private readonly ProductMapper _ProdutsMapper;

        public OrderMapper(ProductMapper produtsMapper)
        {
            _ProdutsMapper = produtsMapper;
        }

        public OrderEntity ToOrderEntity(Order order, ProductEntity produtos)
        {
            return new OrderEntity(
                produtos,
                order.Valor,
                order.Quantity
            );
        }

        public Order ToOrder(OrderEntity orderEntity)
        {
            return new Order(
                orderEntity.Id,
                _ProdutsMapper.ToProduct(orderEntity.Produtos),
                orderEntity.Valor,
                orderEntity.CreatedAt,
                orderEntity.Quantity

            );
        }

        public List<Order> ToOrder(List<OrderEntity> ordersEntities)
        {
            List<Order> orders = [];

            foreach(OrderEntity orderEntity in ordersEntities)
            {
                orders.Add(ToOrder(orderEntity));
            }

            return orders;
        }


        public Order ToOrder(OrderRequestDto orderRequestDto)
        {
            return new Order(
               _ProdutsMapper.ToProdut(orderRequestDto.ProdutoId),
               orderRequestDto.Value,
               orderRequestDto.Quantity
             );
        }


    }
}
