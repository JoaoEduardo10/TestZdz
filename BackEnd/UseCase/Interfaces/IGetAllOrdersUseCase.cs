using Core.Domain;

namespace UseCase.Interfaces
{
    public interface IGetAllOrdersUseCase
    {
        Task<List<Order>> GetAllOrdersAsync(); 
    }
}
