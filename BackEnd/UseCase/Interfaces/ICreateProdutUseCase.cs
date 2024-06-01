using Core.Domain;

namespace UseCase.Interfaces
{
    public interface ICreateProdutUseCase
    {
        Task CreateProdutAsync(Produtos produtos);
    }
}
