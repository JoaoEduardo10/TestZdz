using Application.Gateway;
using Core.Domain;
using Core.Exceptions;
using Core.Exceptions.enums;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class GetOrderByIdGatewayImpl : IGetOrderByIdGateway
    {
        private readonly InfrastrutureDataBaseContext _Context;

        private readonly OrderMapper _OrderMapper;

        public GetOrderByIdGatewayImpl(InfrastrutureDataBaseContext context, OrderMapper orderMapper)
        {
            _Context = context;
            _OrderMapper = orderMapper;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            OrderEntity orderEntity = await _Context.OrdersEntity.Select(
                     order => new OrderEntity
                     {
                         Id = order.Id,
                         ProdutoId = order.ProdutoId,
                         Quantity = order.Quantity,
                         Valor = order.Valor,
                         Produtos = _Context.ProductsEntity.FirstOrDefault(p => p.Id == order.ProdutoId),
                         CreatedAt = order.CreatedAt
                     }
                    ).FirstOrDefaultAsync(o => o.Id == orderId) ??
                throw new OrderException(ErrorCodeEnum.OR0002.GetMessage(), ErrorCodeEnum.OR0002.GetCode());

            Order order = _OrderMapper.ToOrderEntityFromOrder(orderEntity);

            return order;
        }
    }
}
