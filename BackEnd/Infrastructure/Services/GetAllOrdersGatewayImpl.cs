using Application.Gateway;
using Core.Domain;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class GetAllOrdersGatewayImpl : IGetAllOrdersGateway
    {
        private readonly InfrastrutureDataBaseContext _Context;

        private readonly OrderMapper _OrderMapper;

        public GetAllOrdersGatewayImpl(InfrastrutureDataBaseContext context, OrderMapper orderMapper)
        {
            _Context = context;
            _OrderMapper = orderMapper;
        }

        public async Task<List<Order>> GetAllOrderAsync()
        {
            var ordersList = await _Context.OrdersEntity.ToListAsync();

            var orders = new List<OrderEntity>();

            foreach (var order in ordersList)
            {
                ProductEntity? produto = await _Context.ProductsEntity.FirstOrDefaultAsync(P => P.Id == order.ProdutoId);

                orders.Add(new OrderEntity
                {
                    Id = order.Id,
                    ProdutoId = order.ProdutoId,
                    Produtos = produto ,
                    Quantity = order.Quantity,
                    Valor = order.Valor,
                    CreatedAt = order.CreatedAt
                });
            }

            await _Context.SaveChangesAsync();

            return _OrderMapper.ToOrderEntityListFromOrdeList(orders);
        }
    }
}
