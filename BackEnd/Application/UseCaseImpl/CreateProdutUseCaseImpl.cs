using Application.Gateway;
using Core.Domain;
using Core.Exceptions;
using Core.Exceptions.enums;
using UseCase.Interfaces;

namespace Application.UseCaseImpl
{
    public class CreateProdutUseCaseImpl : ICreateProdutUseCase
    {

        private readonly ICreateProdutGateway _CreateProdutGateway;

        public CreateProdutUseCaseImpl(ICreateProdutGateway CreateProdutGateway)
        {
            _CreateProdutGateway = CreateProdutGateway;
        }

        public async Task CreateProdutAsync(Produtos produtos)
        {
            bool produtSaved = await _CreateProdutGateway.CreateProdutAsync(produtos);

            if(!produtSaved)
            {
                throw new ProdutoException(ErrorCodeEnum.PRO0003.GetMessage(), ErrorCodeEnum.PRO0003.GetCode());
            }

            return;
        }
    }

 
}
