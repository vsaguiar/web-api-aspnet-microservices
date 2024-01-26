﻿using Microsoft.AspNetCore.Mvc;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task <ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        var result = await _productService.GetAllProductsAsync();

        if (result is null)
            return View("Error");

        return View(result);
    }
}
