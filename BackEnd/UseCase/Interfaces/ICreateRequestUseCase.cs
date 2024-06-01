using Core.Domain;

namespace UseCase.Interfaces
{
    public interface ICreateRequestUseCase
    {
        Task CreateRequestAsync(Pedidos pedidos);
    }
}
