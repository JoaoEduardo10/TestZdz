using Application.Gateway;
using Core.Exceptions;
using Core.Exceptions.enums;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    public class DeleteOrderByIdUseCaseImpl : IDeleteOrderByIdUseCase
    {
        private readonly IDeleteOrderByIdGateway _DeleteOrderByIdGateway;

        public DeleteOrderByIdUseCaseImpl(IDeleteOrderByIdGateway deleteOrderByIdGateway)
        {
            _DeleteOrderByIdGateway = deleteOrderByIdGateway;
        }

        public async Task DeleteOrderAsync(int orderId)
        {
           bool isOrderDeleted = await _DeleteOrderByIdGateway.DeleteOrderAsync(orderId);


            if (!isOrderDeleted)
            {
                throw new OrderException(ErrorCodeEnum.OR0002.GetMessage(), ErrorCodeEnum.OR0002.GetCode());
            }
        }
    }
}
