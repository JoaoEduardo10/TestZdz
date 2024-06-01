using Core.Domain;

namespace Application.Gateway
{
    public interface IGetAllProdutsGateway
    {
        Task<List<Produtos>> GetAllProdutsAsync();
    }
}
