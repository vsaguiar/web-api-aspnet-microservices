using VShop.ProductApi.DTOs;

namespace VShop.ProductApi.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProductsAsync();
    Task<ProductDTO> GetProductByIdAsync(int id);
    Task AddProductAsync(ProductDTO productDTO);
    Task UpdateProductAsync(ProductDTO productDTO);
    Task RemoveProductAsync(int id);
}
