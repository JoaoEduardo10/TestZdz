using Core.Domain;

namespace Application.Gateway
{
    public interface ICreateRequestGateway
    {
        Task<bool> CreateRequestAsync(Pedidos pedidos);
    }
}
