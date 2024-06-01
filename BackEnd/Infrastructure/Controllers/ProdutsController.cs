using Infrastructure.Dtos.Request;
using Infrastructure.Dtos.Response;
using Infrastructure.Mapper;
using Microsoft.AspNetCore.Mvc;
using UseCase.Interfaces;

namespace Infrastructure.Controllers
{
    [ApiController]
    [Route("/api/v1/produtos")]
    public class ProdutsController : ControllerBase
    {
        private readonly ICreateProdutUseCase _CreateProdutUseCase;

        private readonly ProdutsMapper _ProdutsMapper;

        public ProdutsController(ICreateProdutUseCase createProdutUseCase, ProdutsMapper produtsMapper)
        {
            _CreateProdutUseCase = createProdutUseCase;
            _ProdutsMapper = produtsMapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProdutRequestDto produtDto )
        {
            await _CreateProdutUseCase.CreateProdutAsync(_ProdutsMapper.ToProdut(produtDto));

            return Created();
        }
    }
}
