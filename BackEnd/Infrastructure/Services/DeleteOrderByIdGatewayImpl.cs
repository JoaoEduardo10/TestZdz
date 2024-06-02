using Application.Gateway;
using Infrastructure.Data;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class DeleteOrderByIdGatewayImpl : IDeleteOrderByIdGateway
    {
        private readonly InfrastrutureDataBaseContext _Context;

        public DeleteOrderByIdGatewayImpl(InfrastrutureDataBaseContext context)
        {
            _Context = context;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            OrderEntity? order = await _Context.OrdersEntity.FirstOrDefaultAsync(o => o.Id == orderId);

            if(order == null)
            {
                return false;
            }

            _Context.OrdersEntity.Remove(order);

            await _Context.SaveChangesAsync();

            return true;
        }
    }
}
