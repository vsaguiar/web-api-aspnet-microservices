using System.Text;
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

    public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response
                    .Content
                    .ReadAsStreamAsync();

                listProductsVM = await JsonSerializer
                    .DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _jsonOptions);
            }
            else
            {
                return null;
            }
        }
        return listProductsVM;
    }

    public async Task<ProductViewModel> GetProductByIdAsync(int id)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response
                    .Content
                    .ReadAsStreamAsync();

                productVM = await JsonSerializer
                    .DeserializeAsync<ProductViewModel>(apiResponse, _jsonOptions);
            }
            else
            {
                return null;
            }
        }
        return productVM;
    }

    public async Task<ProductViewModel> CreateProductAsync(ProductViewModel productVM)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        StringContent content = new StringContent(JsonSerializer.Serialize(productVM), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response
                    .Content
                    .ReadAsStreamAsync();

                productVM = await JsonSerializer
                    .DeserializeAsync<ProductViewModel>(apiResponse, _jsonOptions);
            }
            else
            {
                return null;
            }
        }
        return productVM;
    }

    public async Task<ProductViewModel> UpdateProductAsync(ProductViewModel productVM)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        ProductViewModel productUpdated = new ProductViewModel();

        using (var response = await client.PutAsJsonAsync(apiEndpoint, productVM))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response
                    .Content
                    .ReadAsStreamAsync();

                productUpdated = await JsonSerializer
                    .DeserializeAsync<ProductViewModel>(apiResponse, _jsonOptions);
            }
            else
            {
                return null;
            }
        }
        return productUpdated;
    }

    public async Task<bool> DeleteProductByIdAsync(int id)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        return false;
    }
}
