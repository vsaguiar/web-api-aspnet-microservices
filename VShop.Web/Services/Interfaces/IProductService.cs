using VShop.Web.Models;

namespace VShop.Web.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
    Task<ProductViewModel> GetProductByIdAsync(int id);
    Task<ProductViewModel> CreateProductAsync(ProductViewModel productVM);
    Task<ProductViewModel> UpdateProductAsync(ProductViewModel productVM);
    Task<bool> DeleteProductByIdAsync(int id);
}
