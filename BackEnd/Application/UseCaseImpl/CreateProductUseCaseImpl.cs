using Application.Gateway;
using Core.Domain;
using Core.Exceptions;
using Core.Exceptions.enums;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    public class CreateProductUseCaseImpl : ICreateProductUseCase
    {

        private readonly ICreateProductGateway _CreateProductGateway;

        public CreateProductUseCaseImpl(ICreateProductGateway createProductGateway)
        {
            _CreateProductGateway = createProductGateway;
        }

        public async Task CreateProductAsync(Product product)
        {
            bool productSaved = await _CreateProductGateway.CreateProductAsync(product);

            if(!productSaved)
            {
                throw new ProductException(ErrorCodeEnum.PRO0003.GetMessage(), ErrorCodeEnum.PRO0003.GetCode());
            }

            return;
        }
    }

 
}
