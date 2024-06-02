using Application.Gateway;
using Core.Domain;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    public class GetAllProductsUseCseImpl : IGetAllProductUseCase
    {
        private readonly IGetAllProductsGateway _GetAllProductsGateway;

        public GetAllProductsUseCseImpl(IGetAllProductsGateway getAllProductsGateway)
        {
            _GetAllProductsGateway = getAllProductsGateway;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = await _GetAllProductsGateway.GetAllProductsAsync();

            return products;
        }
    }
}
