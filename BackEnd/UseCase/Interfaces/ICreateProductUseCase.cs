using Core.Domain;

namespace UseCase.Interfaces
{
    public interface ICreateProductUseCase
    {
        Task CreateProductAsync(Product product);
    }
}
