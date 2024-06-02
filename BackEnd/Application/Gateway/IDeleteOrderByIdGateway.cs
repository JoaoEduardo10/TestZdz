namespace Application.Gateway
{
    public interface IDeleteOrderByIdGateway
    {
        Task<bool> DeleteOrderAsync(int orderId);
    }
}
