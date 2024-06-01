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
    public class CreateRequestGatewayImpl : ICreateRequestGateway
    {
        private readonly InfrastrutureDataBaseContext _Context;

        private readonly RequestsMapper _RequestsMapper;

        public CreateRequestGatewayImpl(InfrastrutureDataBaseContext context, RequestsMapper requestsMapper)
        {
            _Context = context;
            _RequestsMapper = requestsMapper;
        }

        public async Task<bool> CreateRequestAsync(Pedidos pedidos)
        {
            ProdutosEntity? produt = await _Context.ProdutosEntity.FirstOrDefaultAsync(p => p.Id == pedidos.Produto.Id) ?? throw new ProdutoException(ErrorCodeEnum.PRO0004.GetMessage(), ErrorCodeEnum.PRO0004.GetCode());

            float quantityProdutsValue = pedidos.Quantity * produt.Value;

            if(quantityProdutsValue != pedidos.Valor)
            {
                throw new PedidosException(ErrorCodeEnum.PEO0003.GetMessage(), ErrorCodeEnum.PEO0003.GetCode());
            }

            var requestSaved = _Context.PedidosEntity.Add(_RequestsMapper.ToRequestEntity(pedidos, produt));

            await _Context.SaveChangesAsync();

            if(requestSaved == null)
            {
                return false;
            }

            return true;
        }
    }
}
