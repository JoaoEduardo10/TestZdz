using Application.Gateway;
using Core.Domain;
using Core.Exceptions;
using Core.Exceptions.enums;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    internal class UpdateOrderByIdUseCaseImpl : IUpdateOrderByIdUseCase
    {
        private readonly IUpdateOrderByIdGateway _UpdateOrderByIdGateway;

        public UpdateOrderByIdUseCaseImpl(IUpdateOrderByIdGateway updateOrderByIdGateway)
        {
            _UpdateOrderByIdGateway = updateOrderByIdGateway;
        }

        public async Task UpdateOrderAsync(int orderId, Order order)
        {
            bool orderSaved = await _UpdateOrderByIdGateway.UpdateOrderAsync(orderId, order); 

            if(!orderSaved)
            {
                throw new OrderException(ErrorCodeEnum.OR0002.GetMessage(), ErrorCodeEnum.OR0003.GetCode());
            }

            throw new NotImplementedException();
        }
    }
}
