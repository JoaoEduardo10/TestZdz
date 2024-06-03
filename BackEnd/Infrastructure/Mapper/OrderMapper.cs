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

        public OrderEntity ToOrderFromOrderEntity(Order order, ProductEntity produtos)
        {
            return new OrderEntity(
                produtos,
                order.Valor,
                order.Quantity
            );
        }

        public Order ToOrderEntityFromOrder(OrderEntity orderEntity)
        {
            return new Order(
                orderEntity.Id,
                _ProdutsMapper.ToProductEntityFromProduct(orderEntity.Produtos),
                orderEntity.Valor,
                orderEntity.CreatedAt,
                orderEntity.Quantity

            );
        }

        public List<Order> ToOrderEntityListFromOrdeList(List<OrderEntity> ordersEntities)
        {
            List<Order> orders = [];

            foreach(OrderEntity orderEntity in ordersEntities)
            {
                orders.Add(ToOrderEntityFromOrder(orderEntity));
            }

            return orders;
        }


        public Order ToOrderRequestFtoFromOrder(OrderRequestDto orderRequestDto)
        {
            return new Order(
               _ProdutsMapper.AddIdFromProduct(orderRequestDto.ProdutoId),
               1,
               orderRequestDto.Quantity
             );
        }


    }
}
