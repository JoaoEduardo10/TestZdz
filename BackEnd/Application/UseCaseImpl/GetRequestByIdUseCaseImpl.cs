using Application.Gateway;
using Core.Domain;
using Core.Exceptions;
using Core.Exceptions.enums;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    internal class GetRequestByIdUseCaseImpl : IGetRequestByIdUseCase
    {
        private readonly IGetRequestByIdGateway _GetRequestByIdGateway;

        public GetRequestByIdUseCaseImpl(IGetRequestByIdGateway getRequestByIdGateway)
        {
            _GetRequestByIdGateway = getRequestByIdGateway;
        }

        public async Task<Pedidos> GetRequestByIdAsync(int pedidoId)
        {
           Pedidos pedido = await _GetRequestByIdGateway.GetRequestByIdAsync(pedidoId);

            return pedido ?? throw new PedidosException(ErrorCodeEnum.PEO0002.GetMessage(), ErrorCodeEnum.PEO0002.GetCode());
        }
    }
}
