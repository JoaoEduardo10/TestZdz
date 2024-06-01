using Core.Domain;
using Infrastructure.Dtos.Request;
using Infrastructure.Entities;

namespace Infrastructure.Mapper
{
    public class RequestsMapper
    {
        private readonly ProdutsMapper _ProdutsMapper;

        public RequestsMapper(ProdutsMapper produtsMapper)
        {
            _ProdutsMapper = produtsMapper;
        }

        public PedidosEntity ToRequestEntity(Pedidos pedidos, ProdutosEntity produtos)
        {
            return new PedidosEntity(
                produtos,
                pedidos.Valor,
                pedidos.Quantity
            );
        }


        public Pedidos ToRequest(RequestDto requestDto)
        {
            return new Pedidos(
               _ProdutsMapper.ToProdut(requestDto.ProdutoId),
               requestDto.Value,
               requestDto.Quantity
             );
        }
    }
}
