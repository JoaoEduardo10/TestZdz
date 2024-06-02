using Core.Domain;

namespace Application.Gateway
{
    public interface IUpdateOrderByIdGateway
    {
        Task<bool> UpdateOrderAsync(int orderId, Order order);
    }
}
