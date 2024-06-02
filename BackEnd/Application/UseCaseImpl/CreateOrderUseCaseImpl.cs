using Application.Gateway;
using Core.Domain;
using Core.Exceptions;
using Core.Exceptions.enums;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    public class CreateOrderUseCaseImpl : ICreateOrderUseCase
    {
        private readonly ICreateOrderGateway _CreateOrderGateway;

        public CreateOrderUseCaseImpl(ICreateOrderGateway createOrderGateway)
        {
            _CreateOrderGateway = createOrderGateway;
        }

        public async Task CreateOrdertAsync(Order order)
        {
            bool orderSaved = await _CreateOrderGateway.CreateOrderAsync(order);


            if (!orderSaved)
            {
                throw new OrderException(ErrorCodeEnum.OR0001.GetMessage(), ErrorCodeEnum.OR0001.GetCode());
            }

            return;
        }
    }
}
