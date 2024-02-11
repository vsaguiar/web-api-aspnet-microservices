using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Roles;
using VShop.ProductApi.Services.Interfaces;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var produtosDTO = await _productService.GetProductsAsync();
            if (produtosDTO is null)
                return NotFound("Produto não encontrado.");
            
            return Ok(produtosDTO);
        }


        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var produtoDTO = await _productService.GetProductByIdAsync(id);
            if (produtoDTO is null)
                return NotFound("Produto não encontrado.");

            return Ok(produtoDTO);
        }


        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Post([FromBody] ProductDTO produtoDTO)
        {
            if (produtoDTO is null)
                return BadRequest("Dados inválidos!");

            await _productService.AddProductAsync(produtoDTO);
            return new CreatedAtRouteResult("GetProduct", new { id = produtoDTO.Id }, produtoDTO);
        }


        [HttpPut]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Put([FromBody] ProductDTO produtoDTO)
        {
            if (produtoDTO is null)
                return BadRequest("Dados inválidos!");

            await _productService.UpdateProductAsync(produtoDTO);

            return Ok(produtoDTO);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var produtoDTO = await _productService.GetProductByIdAsync(id);

            if (produtoDTO is null)
                return NotFound("Produto não encontrado.");

            await _productService.RemoveProductAsync(id);

            return Ok(produtoDTO);
        }
    }
}
