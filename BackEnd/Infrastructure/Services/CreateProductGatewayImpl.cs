using Application.Gateway;
using Core.Domain;
using Infrastructure.Data;
using Infrastructure.Mapper;

namespace Infrastructure.Services
{
    public class CreateProductGatewayImpl : ICreateProductGateway
    {
        private readonly InfrastrutureDataBaseContext _Context;
        private readonly ProductMapper _ProductMapper;

        public CreateProductGatewayImpl(InfrastrutureDataBaseContext context, ProductMapper productMapper)
        {
            _Context = context;
            _ProductMapper = productMapper;
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            var productEntitySaved = _Context.ProductsEntity.Add(_ProductMapper.ToProdutEntity(product));

            await _Context.SaveChangesAsync();

            if(productEntitySaved == null )
            {
                return false;
            }


            return true;
        }
    }
}
