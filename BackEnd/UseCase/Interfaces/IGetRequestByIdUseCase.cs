using Core.Domain;

namespace UseCase.Interfaces
{
    public interface IGetRequestByIdUseCase
    {
        Task<Pedidos> GetRequestByIdAsync(int pedidosId);
    }
}
