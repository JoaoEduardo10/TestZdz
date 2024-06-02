using Application.Gateway;
using Core.Exceptions;
using Core.Exceptions.enums;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    public class DeleteProductByIdUseCaseImpl : IDeleteProductByIdUseCase
    {
        private readonly IDeleteProductByIdGateway _DeleteProductByIdGateway;

        public DeleteProductByIdUseCaseImpl(IDeleteProductByIdGateway deleteProductByIdGateway)
        {
            _DeleteProductByIdGateway = deleteProductByIdGateway;
        }

        public async Task DeleteProductAsync(int productId)
        {
            bool isProductDeleted = await _DeleteProductByIdGateway.DeleteProductAsync(productId);

            if(!isProductDeleted)
            {
                throw new ProductException(ErrorCodeEnum.PRO0006.GetMessage(), ErrorCodeEnum.PRO0006.GetMessage());
            }
        }
    }
}
