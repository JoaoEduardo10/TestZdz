using Application.Gateway;
using Infrastructure.Data;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class DeleteProductByIdGatewayImpl : IDeleteProductByIdGateway
    {
        private readonly InfrastrutureDataBaseContext _Context;

        public DeleteProductByIdGatewayImpl(InfrastrutureDataBaseContext context)
        {
            _Context = context;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            ProductEntity? product = await _Context.ProductsEntity.FirstOrDefaultAsync(p => p.Id == productId);

            if(product == null)
            {
                return false;
            }

            List<OrderEntity> order = await _Context.OrdersEntity.Where(o => o.ProdutoId == product.Id).ToListAsync();

            foreach(OrderEntity orderEntity in order)
            {
                _Context.OrdersEntity.Remove(orderEntity);
            }

            _Context.ProductsEntity.Remove(product);

            await _Context.SaveChangesAsync();

            return true;
        }
    }
}
