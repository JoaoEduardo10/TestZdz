using Core.Domain;

namespace Application.Gateway
{
    public interface IGetOrderByIdGateway
    {
        Task<Order> GetOrderByIdAsync(int orderId);
    }
}
