using Core.Domain;

namespace Application.Gateway
{
    public interface IGetAllRequetsGateway
    {
        Task<List<Pedidos>> GetAllRequestsAsync();
    }
}
