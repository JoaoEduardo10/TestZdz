using Core.Domain;

namespace UseCase.Interfaces
{
    public interface ICreateOrderUseCase
    {
        Task CreateOrdertAsync(Order order);
    }
}
