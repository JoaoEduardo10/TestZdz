using Core.Domain;
using Infrastructure.Dtos.Request;
using Infrastructure.Dtos.Response;
using Infrastructure.Mapper;
using Microsoft.AspNetCore.Mvc;
using UseCase.Interfaces;

namespace Infrastructure.Controllers
{
    [ApiController]
    [Route("/api/v1/product")]
    public class ProductController : ControllerBase
    {
        private readonly ICreateProductUseCase _CreateProductUseCase;
        private readonly IGetAllProductUseCase _GetAllProductUseCase;

        private readonly ProductMapper _ProdutsMapper;

        public ProductController(ICreateProductUseCase createProductUseCase, ProductMapper productMapper, IGetAllProductUseCase getAllProductUseCase)
        {
            _CreateProductUseCase = createProductUseCase;
            _ProdutsMapper = productMapper;
            _GetAllProductUseCase = getAllProductUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<Product>>>> GetAll()
        {
            List<Product> products = await _GetAllProductUseCase.GetAllProductsAsync();

            BaseResponse<List<Product>> response = new BaseResponse<List<Product>>()
            {
                Message = "Produtos listados com sucesso!",
                Result = products,
                StatusCode = 200,
                Success = true
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductRequestDto productRequestDto )
        {
            await _CreateProductUseCase.CreateProductAsync(_ProdutsMapper.ToProdut(productRequestDto));

            return Created();
        }
    }
}
