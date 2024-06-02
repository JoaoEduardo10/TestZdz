using Core.Domain;

namespace Application.Gateway
{
    public interface IUpdateProductByIdGateway
    {
        Task<bool> UpdateProductAsync(int productId, Product product);
    }
}
