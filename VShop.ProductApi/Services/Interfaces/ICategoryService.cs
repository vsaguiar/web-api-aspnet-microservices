using VShop.ProductApi.DTOs;

namespace VShop.ProductApi.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
    Task<IEnumerable<CategoryDTO>> GetCategoriesProductsAsync();
    Task<CategoryDTO> GetCategoriesByIdAsync(int id);
    Task AddCategoryAsync(CategoryDTO category);
    Task UpdateCategoryAsync(CategoryDTO category);
    Task RemoveCategoryAsync(int id);
}
