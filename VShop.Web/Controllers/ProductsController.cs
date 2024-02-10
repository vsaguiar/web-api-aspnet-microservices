using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Roles;
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
        var result = await _productService.GetAllProductsAsync(await GetAccessToken());

        if (result is null)
            return View("Error");

        return View(result);
    }

    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategoriesAsync(await GetAccessToken()), "CategoryId", "Name");

        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.CreateProductAsync(productVM, await GetAccessToken());

            if (result != null)
                return RedirectToAction(nameof(Index));
        }
        else
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategoriesAsync(await GetAccessToken()), "CategoryId", "Name");
        }

        return View(productVM);
    }

    public async Task<IActionResult> UpdateProduct(int id)
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategoriesAsync(await GetAccessToken()), "CategoryId", "Name");

        var result = await _productService.GetProductByIdAsync(id, await GetAccessToken());

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.UpdateProductAsync(productVM, await GetAccessToken());

            if (result is not null) 
                return RedirectToAction(nameof(Index));
        }
        return View(productVM);
    }

    [Authorize]
    public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
    {
        var result = await _productService.GetProductByIdAsync(id, await GetAccessToken());
        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost(), ActionName("DeleteProduct")]
    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _productService.DeleteProductByIdAsync(id, await GetAccessToken());

        if (!result) 
            return View("Error");

        return RedirectToAction("Index");
    }

    private async Task<string> GetAccessToken()
    {
        return await HttpContext.GetTokenAsync("access_token");
    }
}
