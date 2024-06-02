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
    public class CreateOrderGatewayImpl : ICreateOrderGateway
    {
        private readonly InfrastrutureDataBaseContext _Context;

        private readonly OrderMapper _OrderMapper;

        public CreateOrderGatewayImpl(InfrastrutureDataBaseContext context, OrderMapper orderMapper)
        {
            _Context = context;
            _OrderMapper = orderMapper;
        }

        public async Task<bool> CreateOrderAsync(Order order)
        {
            ProductEntity? produt = await _Context.ProductsEntity.FirstOrDefaultAsync(p => p.Id == order.Product.Id) ?? throw new OrderException(ErrorCodeEnum.PRO0004.GetMessage(), ErrorCodeEnum.PRO0004.GetCode());

            float quantityProdutsValue = order.Quantity * produt.Value;

            if(quantityProdutsValue != order.Valor)
            {
                throw new OrderException(ErrorCodeEnum.OR0003.GetMessage(), ErrorCodeEnum.OR0003.GetCode());
            }

            var orderSaved = _Context.OrdersEntity.Add(_OrderMapper.ToOrderEntity(order, produt));

            await _Context.SaveChangesAsync();

            if(orderSaved == null)
            {
                return false;
            }

            return true;
        }
    }
}
