using Core.Domain;

namespace UseCase.Interfaces
{
    public interface IGetOrderByIdUseCase
    {
        Task<Order> GetOrderByIdAsync(int orderId);
    }
}
