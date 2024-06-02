namespace UseCase.Interfaces
{
    public interface IDeleteProductByIdUseCase
    {
        Task DeleteProductAsync(int  productId);
    }
}
