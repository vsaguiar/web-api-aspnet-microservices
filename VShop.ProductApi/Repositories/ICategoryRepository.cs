using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<IEnumerable<Category>> GetCategoriesProductsAsync();
    Task<Category> GetCategoryByIdAsync(int id);
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category> UpdateCategoryAsync(Category category);
    Task<Category> DeleteCategoryAsync(int id);
}
