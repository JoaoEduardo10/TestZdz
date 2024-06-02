using Core.Domain;

namespace Application.Gateway
{
    public interface ICreateProductGateway
    {
        Task<bool> CreateProductAsync(Product product);
    }
}