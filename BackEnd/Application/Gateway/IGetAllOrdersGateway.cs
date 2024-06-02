using Core.Domain;

namespace Application.Gateway
{
    public interface IGetAllOrdersGateway
    {
        Task<List<Order>> GetAllOrderAsync();
    }
}
