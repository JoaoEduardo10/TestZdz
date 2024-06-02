using Core.Domain;
using Infrastructure.Dtos.Request;
using Infrastructure.Mapper;
using Microsoft.AspNetCore.Mvc;
using UseCase.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Infrastructure.Controllers
{
    [Route("/api/v1/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly ICreateOrderUseCase _CreateOrderUseCase;


        private readonly OrderMapper _OrderMapper;


        public OrderController(ICreateOrderUseCase createOrderUseCase, OrderMapper orderMapper)
        {
            _CreateOrderUseCase = createOrderUseCase;
            _OrderMapper = orderMapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] OrderRequestDto orderRequestDtos)
        {
            Order pedido = _OrderMapper.ToRequest(orderRequestDtos);

            await _CreateOrderUseCase.CreateOrdertAsync(pedido);

            return Created();
        }
    }
}
