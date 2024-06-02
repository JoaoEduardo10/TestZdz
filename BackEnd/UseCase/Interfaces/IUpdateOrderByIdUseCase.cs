namespace UseCase.Interfaces
{
    public interface IUpdateOrderByIdUseCase
    {
        Task UpdateOrderAsync(int orderId);
    }
}
