using Application.Gateway;
using Core.Domain;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    public class GetAllOrdersUseCaseImpl : IGetAllOrdersUseCase
    {
        private readonly IGetAllOrdersGateway _GetAllOrdersGateway;

        public GetAllOrdersUseCaseImpl(IGetAllOrdersGateway getAllOrdersGateway)
        {
            _GetAllOrdersGateway = getAllOrdersGateway;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            List<Order> Orders = await _GetAllOrdersGateway.GetAllOrderAsync();

            return Orders;
        }
    }
}
