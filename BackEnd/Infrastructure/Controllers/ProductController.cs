using Infrastructure.Dtos.Request;
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

        private readonly ProductMapper _ProdutsMapper;

        public ProductController(ICreateProductUseCase createProductUseCase, ProductMapper productMapper)
        {
            _CreateProductUseCase = createProductUseCase;
            _ProdutsMapper = productMapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductRequestDto productRequestDto )
        {
            await _CreateProductUseCase.CreateProductAsync(_ProdutsMapper.ToProdut(productRequestDto));

            return Created();
        }
    }
}
