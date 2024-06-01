using Application.Gateway;
using Core.Domain;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    public class GetAllProdutsUseCseImpl : IGetAllProdutsUseCase
    {
        private readonly IGetAllProdutsGateway _GetAllProdutsGateway;

        public GetAllProdutsUseCseImpl(IGetAllProdutsGateway getAllProdutsGateway)
        {
            _GetAllProdutsGateway = getAllProdutsGateway;
        }

        public async Task<List<Produtos>> GetAllProdutsAsync()
        {
            List<Produtos> produtos = await _GetAllProdutsGateway.GetAllProdutsAsync();


            return produtos;
        }
    }
}
