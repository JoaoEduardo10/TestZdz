using Application.Gateway;
using Core.Domain;
using Core.Exceptions;
using Core.Exceptions.enums;
using Infrastructure.Data;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UpdateOrderByIdGatewayImpl : IUpdateOrderByIdGateway
    {
        private readonly InfrastrutureDataBaseContext _Context;

        public UpdateOrderByIdGatewayImpl(InfrastrutureDataBaseContext context)
        {
            _Context = context;
        }

        public async Task<bool> UpdateOrderAsync(int orderId, Order order)
        {
            OrderEntity? orderEntity = await _Context.OrdersEntity.FirstOrDefaultAsync(o => o.Id == orderId);

            if(orderEntity == null)
            {
                return false;
            }

            ProductEntity produt = await _Context.ProductsEntity.FirstOrDefaultAsync(p => p.Id == order.Product.Id)
                ?? throw new ProductException(ErrorCodeEnum.PRO0004.GetMessage(), ErrorCodeEnum.PRO0004.GetMessage());

            orderEntity.Quantity = order.Quantity;
            orderEntity.ProdutoId = order.Product.Id;
            orderEntity.Valor = order.Quantity * produt.Value;

            await _Context.SaveChangesAsync();

            return true;
        }
    }
}
