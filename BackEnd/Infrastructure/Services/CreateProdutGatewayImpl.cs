using Application.Gateway;
using Core.Domain;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Mapper;

namespace Infrastructure.Services
{
    public class CreateProdutGatewayImpl : ICreateProdutGateway
    {
        private readonly InfrastrutureDataBaseContext _Context;
        private readonly ProdutsMapper _ProdutsMapper;

        public CreateProdutGatewayImpl(InfrastrutureDataBaseContext context, ProdutsMapper produtsMapper)
        {
            _Context = context;
            _ProdutsMapper = produtsMapper;
        }

        public async Task<bool> CreateProdutAsync(Produtos produtos)
        {
            var produtosEntitySaved = _Context.ProdutosEntity.Add(_ProdutsMapper.ToProdutEntity(produtos));

            await _Context.SaveChangesAsync();

            if(produtosEntitySaved == null )
            {
                return false;
            }


            return true;
        }
    }
}
