using Core.Domain;
using Infrastructure.Dtos.Request;
using Infrastructure.Mapper;
using Microsoft.AspNetCore.Mvc;
using UseCase.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Infrastructure.Controllers
{
    [Route("/api/v1/pedidos")]
    [ApiController]
    public class RequetsController : ControllerBase
    {

        private readonly ICreateRequestUseCase _CreateRequestUseCase;


        private readonly RequestsMapper _RequestMapper;


        public RequetsController(ICreateRequestUseCase createRequestUseCase, RequestsMapper requestMapper)
        {
            _CreateRequestUseCase = createRequestUseCase;
            _RequestMapper = requestMapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] RequestDto requestDtos)
        {
            Pedidos pedido = _RequestMapper.ToRequest(requestDtos);

            await _CreateRequestUseCase.CreateRequestAsync(pedido);

            return Created();
        }
    }
}
