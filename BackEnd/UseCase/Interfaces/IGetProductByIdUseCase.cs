using Core.Domain;

namespace UseCase.Interfaces
{
    public interface IGetProductByIdUseCase
    {
        Task<Product> GetProductByIdAsync(int produtId);
    }
}
