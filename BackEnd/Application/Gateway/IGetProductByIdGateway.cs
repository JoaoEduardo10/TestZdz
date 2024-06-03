
using Core.Domain;

namespace Application.Gateway
{
    public interface IGetProductByIdGateway
    {
        Task<Product?> GetProductByIdAsync(int productId);
    }
}
