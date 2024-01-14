using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Context;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;
    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return  await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories.Where(c => c.CategoryId == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Category>> GetCategoriesProductsAsync()
    {
        return await _context.Categories.Include(p => p.Products).ToListAsync();
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> DeleteCategoryAsync(int id)
    {
        var category = await GetCategoryByIdAsync(id);
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return category;
    }
}
