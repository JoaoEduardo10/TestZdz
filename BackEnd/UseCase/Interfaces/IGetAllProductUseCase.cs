using Core.Domain;

namespace UseCase.Interfaces
{
    public interface IGetAllProductUseCase
    {
        Task<List<Product>> GetAllProductsAsync();
    }
}
