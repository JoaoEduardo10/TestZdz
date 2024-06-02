using Application.Gateway;
using Core.Domain;
using Core.Exceptions;
using Core.Exceptions.enums;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    public class GetOrderByIdUseCaseImpl : IGetOrderByIdUseCase
    {
        private readonly IGetOrderByIdGateway _GetOrderByIdGateway;

        public GetOrderByIdUseCaseImpl(IGetOrderByIdGateway getOrderByIdGateway)
        {
            _GetOrderByIdGateway = getOrderByIdGateway;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
           Order order = await _GetOrderByIdGateway.GetOrderByIdAsync(orderId);

            return order ?? throw new OrderException(ErrorCodeEnum.OR0002.GetMessage(), ErrorCodeEnum.OR0002.GetCode());
        }
    }
}
