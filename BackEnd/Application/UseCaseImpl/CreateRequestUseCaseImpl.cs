using Application.Gateway;
using Core.Domain;
using Core.Exceptions;
using Core.Exceptions.enums;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    public class CreateRequestUseCaseImpl : ICreateRequestUseCase
    {
        private readonly ICreateRequestGateway _CreateRequestGateway;

        public CreateRequestUseCaseImpl(ICreateRequestGateway createRequestGateway)
        {
            _CreateRequestGateway = createRequestGateway;
        }

        public async Task CreateRequestAsync(Pedidos pedidos)
        {
            bool requestSaved = await _CreateRequestGateway.CreateRequestAsync(pedidos);


            if (!requestSaved)
            {
                throw new PedidosException(ErrorCodeEnum.PEO0001.GetMessage(), ErrorCodeEnum.PEO0001.GetCode());
            }

            return;
        }
    }
}
