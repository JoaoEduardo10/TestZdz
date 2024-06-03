using Core.Domain;
using Infrastructure.Dtos.Request;
using Infrastructure.Dtos.Response;
using Infrastructure.Mapper;
using Microsoft.AspNetCore.Mvc;
using UseCase.Interfaces;

namespace Infrastructure.Controllers
{
    [Route("/api/v1/order/")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly ICreateOrderUseCase _CreateOrderUseCase;
        private readonly IGetAllOrdersUseCase _GetAllOrdersUseCase;
        private readonly IGetOrderByIdUseCase _GetOrderByIdUseCase;
        private readonly IUpdateOrderByIdUseCase _UpdateOrderByIdUseCase;
        private readonly IDeleteOrderByIdUseCase _DeleteOrderByIdUseCase;

        private readonly OrderMapper _OrderMapper;


        public OrderController(ICreateOrderUseCase createOrderUseCase, OrderMapper orderMapper, IGetAllOrdersUseCase getAllOrdersUseCase, IGetOrderByIdUseCase getOrderByIdUseCase, IUpdateOrderByIdUseCase updateOrderByIdUseCase, IDeleteOrderByIdUseCase deleteOrderByIdUseCase)
        {
            _CreateOrderUseCase = createOrderUseCase;
            _OrderMapper = orderMapper;
            _GetAllOrdersUseCase = getAllOrdersUseCase;
            _GetOrderByIdUseCase = getOrderByIdUseCase;
            _UpdateOrderByIdUseCase = updateOrderByIdUseCase;
            _DeleteOrderByIdUseCase = deleteOrderByIdUseCase;
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
           Order order = await _GetOrderByIdUseCase.GetOrderByIdAsync(orderId);

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
            Order order = _OrderMapper.ToOrderRequestFtoFromOrder(orderRequestDtos);

            await _CreateOrderUseCase.CreateOrdertAsync(order);

            return Created();
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult> Update(int orderId, [FromBody] OrderRequestDto orderRequestDto)
        {
            Order order = _OrderMapper.ToOrderRequestFtoFromOrder(orderRequestDto);

            await _UpdateOrderByIdUseCase.UpdateOrderAsync(orderId, order);

            return NoContent();
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> Delete(int orderId)
        {

            await _DeleteOrderByIdUseCase.DeleteOrderAsync(orderId);

            return NoContent();
        }
    }
}
