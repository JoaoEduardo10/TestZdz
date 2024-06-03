using Application.Gateway;
using Core.Domain;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class GetAllProductsGatewayImpl : IGetAllProductsGateway
    {
        private readonly InfrastrutureDataBaseContext _Context;

        private readonly ProductMapper _ProductMapper;

        public GetAllProductsGatewayImpl(InfrastrutureDataBaseContext context, ProductMapper productMapper)
        {
            _Context = context;
            _ProductMapper = productMapper;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = _ProductMapper.ToProductEntityListFromProductList(await _Context.ProductsEntity.ToListAsync());

            return products;
        }
    }
}
