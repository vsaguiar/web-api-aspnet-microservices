using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Roles;
using VShop.ProductApi.Services.Interfaces;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categoriesDTO = await _categoryService.GetCategoriesAsync();
            if (categoriesDTO is null)
                return NotFound("Categoria não encontrada.");
         
            return Ok(categoriesDTO);
        }


        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
        {
            var categoriesDTO = await _categoryService.GetCategoriesProductsAsync();
            if (categoriesDTO is null)
                return NotFound("Categoria não encontrada.");

            return Ok(categoriesDTO);
        }


        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categoriesDTO = await _categoryService.GetCategoriesByIdAsync(id);
            if (categoriesDTO is null)
                return NotFound("Categoria não encontrada.");

            return Ok(categoriesDTO);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
                return BadRequest("Dados inválidos!");

            await _categoryService.AddCategoryAsync(categoryDTO);

            return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.CategoryId }, categoryDTO);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
                return BadRequest();
            
            if (categoryDTO is null)
                return BadRequest();

            await _categoryService.UpdateCategoryAsync(categoryDTO);
            return Ok(categoryDTO);
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Delete(int id)
        {
            var categoryDTO = await _categoryService.GetCategoriesByIdAsync(id);
            if (categoryDTO is null)
                return NotFound("Categoria não encontrada.");

            await _categoryService.RemoveCategoryAsync(id);
            return Ok(categoryDTO);
        }
    }
}
