using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/products/";
    private readonly JsonSerializerOptions _jsonOptions;
    private ProductViewModel productVM;
    private IEnumerable<ProductViewModel> listProductsVM;

    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; // não considera caixa alta e/ou caixa baixa
    }

    public Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> CreateProductAsync(ProductViewModel productVM)
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> UpdateProductAsync(ProductViewModel productVM)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
