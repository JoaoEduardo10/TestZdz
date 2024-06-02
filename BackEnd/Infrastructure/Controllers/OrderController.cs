using Application.Gateway;
using Core.Domain;
using Infrastructure.Dtos.Request;
using Infrastructure.Dtos.Response;
using Infrastructure.Mapper;
using Microsoft.AspNetCore.Mvc;
using UseCase.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Infrastructure.Controllers
{
    [Route("/api/v1/order/")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly ICreateOrderUseCase _CreateOrderUseCase;
        private readonly IGetAllOrdersUseCase _GetAllOrdersUseCase;
        private readonly IGetOrderByIdGateway _GetOrderByIdGateway;

        private readonly OrderMapper _OrderMapper;


        public OrderController(ICreateOrderUseCase createOrderUseCase, OrderMapper orderMapper, IGetAllOrdersUseCase getAllOrdersUseCase, IGetOrderByIdGateway getOrderByIdGateway)
        {
            _CreateOrderUseCase = createOrderUseCase;
            _OrderMapper = orderMapper;
            _GetAllOrdersUseCase = getAllOrdersUseCase;
            _GetOrderByIdGateway = getOrderByIdGateway;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<Order>>>> GetAll()
        {
            List<Order> orders = await _GetAllOrdersUseCase.GetAllOrdersAsync();

            BaseResponse<List<Order>> response = new BaseResponse<List<Order>>
            {
                Message = "Pedidos Listados com sucesso!",
                Result = orders,
                StatusCode = 200,
                Success = true
            };

            return Ok(response);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<BaseResponse<Order>>> GetById(int orderId)
        {
           Order order = await _GetOrderByIdGateway.GetOrderByIdAsync(orderId);

            BaseResponse<Order> response = new BaseResponse<Order>
            {
                Message = "Pedido Encontrado com sucesso!",
                Result = order,
                StatusCode = 200,
                Success = true
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] OrderRequestDto orderRequestDtos)
        {
            Order pedido = _OrderMapper.ToOrder(orderRequestDtos);

            await _CreateOrderUseCase.CreateOrdertAsync(pedido);

            return Created();
        }
    }
}
