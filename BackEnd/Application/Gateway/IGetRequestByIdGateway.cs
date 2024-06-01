using Core.Domain;

namespace Application.Gateway
{
    public interface IGetRequestByIdGateway
    {
        Task<Pedidos> GetRequestByIdAsync(int pedidoId);
    }
}
