using Core.Domain;

namespace Application.Gateway
{
    public interface ICreateProdutGateway
    {
        Task<bool> CreateProdutAsync(Produtos produtos);
    }
}