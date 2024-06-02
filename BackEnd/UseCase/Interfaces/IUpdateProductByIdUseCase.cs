using Core.Domain;

namespace UseCase.Interfaces
{
    public interface IUpdateProductByIdUseCase
    {
        Task UpdateProductAsync(int productId, Product product);
    }
}
