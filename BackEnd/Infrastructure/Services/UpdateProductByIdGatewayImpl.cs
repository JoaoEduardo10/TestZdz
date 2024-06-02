using Application.Gateway;
using Core.Domain;
using Infrastructure.Data;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UpdateProductByIdGatewayImpl : IUpdateProductByIdGateway
    {
        private readonly InfrastrutureDataBaseContext _Context;

        public UpdateProductByIdGatewayImpl(InfrastrutureDataBaseContext context)
        {
            _Context = context;
        }

        public async Task<bool> UpdateProductAsync(int productId, Product product)
        {
            ProductEntity? productEntity = await _Context.ProductsEntity.FirstOrDefaultAsync(p => p.Id == productId);

            if(productEntity == null)
            {
                return false;
            }

            productEntity.Value = product.Value;
            productEntity.Name = product.Name; 

            await _Context.SaveChangesAsync();

            return true;
        }
    }
}
