using Application.Gateway;
using Core.Domain;
using Core.Exceptions;
using Core.Exceptions.enums;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    public class GetProductByIdUseCaseImpl : IGetProductByIdUseCase
    {
        private readonly IGetProductByIdGateway _GetProductByIdGateway;

        public GetProductByIdUseCaseImpl(IGetProductByIdGateway getProductByIdGateway)
        {
            _GetProductByIdGateway = getProductByIdGateway;
        }

        public async Task<Product> GetProductByIdAsync(int produtId)
        {
            Product product = await _GetProductByIdGateway.GetProductByIdAsync(produtId) 
                ?? throw new ProductException(ErrorCodeEnum.PRO0004.GetMessage(), ErrorCodeEnum.PRO0004.GetMessage());

            return product;
        }
    }
}
