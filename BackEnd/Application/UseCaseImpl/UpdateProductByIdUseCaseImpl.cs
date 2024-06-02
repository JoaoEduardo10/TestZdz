

using Application.Gateway;
using Core.Domain;
using Core.Exceptions;
using Core.Exceptions.enums;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    public class UpdateProductByIdUseCaseImpl : IUpdateProductByIdUseCase
    {
        private readonly IUpdateProductByIdGateway _UpdateProductByIdGateway;

        public UpdateProductByIdUseCaseImpl(IUpdateProductByIdGateway updateProductByIdGateway)
        {
            _UpdateProductByIdGateway = updateProductByIdGateway;
        }

        public async Task UpdateProductAsync(int productId, Product product)
        {
            bool isNewProductSaved = await _UpdateProductByIdGateway.UpdateProductAsync(productId, product);

            if(isNewProductSaved)
            {
                throw new ProductException(ErrorCodeEnum.PRO0004.GetMessage(), ErrorCodeEnum.PRO0004.GetMessage());
            }
        }
    }
}
