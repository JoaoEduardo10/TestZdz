using Core.Domain;

namespace UseCase.Interfaces
{
    public interface IGetAllProdutsUseCase
    {
        Task<List<Produtos>> GetAllProdutsAsync();
    }
}
