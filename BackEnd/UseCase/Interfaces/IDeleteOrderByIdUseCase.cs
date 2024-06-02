namespace UseCase.Interfaces
{
    public interface IDeleteOrderByIdUseCase
    {
        Task DeleteOrderAsync(int orderId);
    }
}
