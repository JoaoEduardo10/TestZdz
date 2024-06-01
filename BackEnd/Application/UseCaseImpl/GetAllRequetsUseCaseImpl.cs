using Application.Gateway;
using Core.Domain;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    public class GetAllRequetsUseCaseImpl : IGetAllRequestsUseCase
    {
        private readonly IGetAllRequetsGateway _GetAllRequetsGateway;

        public GetAllRequetsUseCaseImpl(IGetAllRequetsGateway getAllRequetsGateway)
        {
            _GetAllRequetsGateway = getAllRequetsGateway;
        }

        public async Task<List<Pedidos>> GetAllRequetsAsync()
        {
            List<Pedidos> pedidos = await _GetAllRequetsGateway.GetAllRequestsAsync();

            return pedidos;
        }
    }
}
