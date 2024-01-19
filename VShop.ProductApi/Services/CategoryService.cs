using AutoMapper;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories;
using VShop.ProductApi.Services.Interfaces;

namespace VShop.ProductApi.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
    {
        var categoriesEntity = await _categoryRepository.GetCategoriesAsync();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesProductsAsync()
    {
        var categoriesEntity = await _categoryRepository.GetCategoriesProductsAsync();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
    }

    public async Task<CategoryDTO> GetCategoriesByIdAsync(int id)
    {
        var categoriesEntity = await _categoryRepository.GetCategoryByIdAsync(id);
        return _mapper.Map<CategoryDTO>(categoriesEntity);
    }

    public async Task AddCategoryAsync(CategoryDTO categoryDTO)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDTO);
        await _categoryRepository.CreateCategoryAsync(categoryEntity);
        categoryDTO.CategoryId = categoryEntity.CategoryId;
    }

    public async Task UpdateCategoryAsync(CategoryDTO categoryDTO)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDTO);
        await _categoryRepository.UpdateCategoryAsync(categoryEntity);
    }

    public async Task RemoveCategoryAsync(int id)
    {
        var categoryEntity = _categoryRepository.GetCategoryByIdAsync(id).Result;
        await _categoryRepository.DeleteCategoryAsync(categoryEntity.CategoryId);
    }
    
}
