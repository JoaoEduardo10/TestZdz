using Core.Domain;

namespace UseCase.Interfaces
{
    public interface IGetRequestByProdutsUseCase
    {
        Task<List<Pedidos>> GetRequestByProdutsIdAsync(int  produtId);
    }
}
