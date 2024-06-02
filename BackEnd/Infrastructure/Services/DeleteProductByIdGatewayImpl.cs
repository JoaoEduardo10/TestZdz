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

            _Context.ProductsEntity.Remove(product);

            await _Context.SaveChangesAsync();

            return true;
        }
    }
}
