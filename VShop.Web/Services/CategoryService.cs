using System.Net.Http.Headers;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services;

public class CategoryService : ICategoryService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/categories/";
    private readonly JsonSerializerOptions _jsonOptions;

    public CategoryService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; // não considera caixa alta e/ou caixa baixa
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync(string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        IEnumerable<CategoryViewModel> categories;

        var response = await client.GetAsync(apiEndpoint);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response
                .Content
                .ReadAsStreamAsync();

            categories = await JsonSerializer
                .DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _jsonOptions);
        }
        else
        {
            return null;
        }

        return categories;
    }
}
