using Core.Domain;

namespace Application.Gateway
{
    public interface ICreateOrderGateway
    {
        Task<bool> CreateOrderAsync(Order order);
    }
}
