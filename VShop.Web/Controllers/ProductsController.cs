using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductsController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task <ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        var result = await _productService.GetAllProductsAsync();

        if (result is null)
            return View("Error");

        return View(result);
    }

    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.CreateProductAsync(productVM);

            if (result != null)
                return RedirectToAction(nameof(Index));
        }
        else
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "Name");
        }

        return View(productVM);
    }
}
