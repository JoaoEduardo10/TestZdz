namespace Application.Gateway
{
    public interface IDeleteProductByIdGateway
    {
        Task<bool> DeleteProductAsync(int productId);
    }
}
