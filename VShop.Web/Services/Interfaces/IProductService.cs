using VShop.Web.Models;

namespace VShop.Web.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProductsAsync(string token);
    Task<ProductViewModel> GetProductByIdAsync(int id, string token);
    Task<ProductViewModel> CreateProductAsync(ProductViewModel productVM, string token);
    Task<ProductViewModel> UpdateProductAsync(ProductViewModel productVM, string token);
    Task<bool> DeleteProductByIdAsync(int id, string token);
}
