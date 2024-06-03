using Application.Gateway;
using Core.Domain;
using Infrastructure.Data;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Entities;

namespace Infrastructure.Services
{
    public class GetProductByIdGatewayImpl : IGetProductByIdGateway
    {
        private readonly InfrastrutureDataBaseContext _Context;

        private readonly ProductMapper _ProductMapper;

        public GetProductByIdGatewayImpl(InfrastrutureDataBaseContext context, ProductMapper productMapper)
        {
            _Context = context;
            _ProductMapper = productMapper;
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            ProductEntity? product = await _Context.ProductsEntity.FirstOrDefaultAsync(p => p.Id == productId);

            if(product is null)
            {
                return null;
            }

            return _ProductMapper.ToProductEntityFromProduct(product);
        }
    }
}
