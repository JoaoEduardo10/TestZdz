using Core.Domain;
using Infrastructure.Dtos.Request;
using Infrastructure.Entities;

namespace Infrastructure.Mapper
{
    public class ProdutsMapper
    {
        public Produtos ToProdut(ProdutosEntity produtosEntity)
        {
            return new Produtos(
             produtosEntity.Id,
             produtosEntity.Name,
             produtosEntity.Value
             );
        }

        public Produtos ToProdut(ProdutRequestDto produtosEntity)
        {
            return new Produtos(
             produtosEntity.Name,
             produtosEntity.Value
             );
        }

        public Produtos ToProdut(int id)
        {
            return new Produtos(
             id
             );
        }

        public ProdutosEntity ToProdutEntity(Produtos produtos)
        {
            return new ProdutosEntity(
             produtos.Name,
             produtos.Value
             );
        }

    }
}
