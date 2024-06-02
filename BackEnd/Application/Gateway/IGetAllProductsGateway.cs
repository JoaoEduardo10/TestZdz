using Core.Domain;

namespace Application.Gateway
{
    public interface IGetAllProductsGateway
    {
        Task<List<Product>> GetAllProductsAsync();
    }
}
