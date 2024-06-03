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
        private readonly IUpdateProductByIdUseCase _UpdateProductByIdUseCase;
        private readonly IDeleteProductByIdUseCase _DeleteProductByIdUseCas;
        private readonly IGetProductByIdUseCase _GetProductByIdUseCase;

        private readonly ProductMapper _ProdutsMapper;

        public ProductController(ICreateProductUseCase createProductUseCase, ProductMapper productMapper, IGetAllProductUseCase getAllProductUseCase, IDeleteProductByIdUseCase deleteProductByIdUseCas, IUpdateProductByIdUseCase updateProductByIdUseCase, IGetProductByIdUseCase getProductByIdUseCase)
        {
            _CreateProductUseCase = createProductUseCase;
            _ProdutsMapper = productMapper;
            _GetAllProductUseCase = getAllProductUseCase;
            _DeleteProductByIdUseCas = deleteProductByIdUseCas;
            _UpdateProductByIdUseCase = updateProductByIdUseCase;
            _GetProductByIdUseCase = getProductByIdUseCase;
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

        [HttpGet("{productId}")]
        public async Task<ActionResult<BaseResponse<Product>>> GetById(int productId)
        {
            Product product =  await _GetProductByIdUseCase.GetProductByIdAsync(productId);

            BaseResponse<Product> response = new BaseResponse<Product>()
            {
                Message = "Sucesso na busca do produto!",
                Result = product,
                StatusCode = 200,
                Success = true
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductRequestDto productRequestDto )
        {
            await _CreateProductUseCase.CreateProductAsync(_ProdutsMapper.ToProductRequestDtoFromProduct(productRequestDto));

            return Created();
        }

        [HttpPut("{productId}")]
        public async Task<ActionResult> Update(int productId, [FromBody] ProductRequestDto productRequestDto)
        {
            await _UpdateProductByIdUseCase.UpdateProductAsync(productId,_ProdutsMapper.ToProductRequestDtoFromProduct(productRequestDto));

            return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> Delete(int productId)
        {
            await _DeleteProductByIdUseCas.DeleteProductAsync(productId);

            return NoContent();
        }
    }
}
