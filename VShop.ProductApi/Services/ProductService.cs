using AutoMapper;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories;
using VShop.ProductApi.Services.Interfaces;

namespace VShop.ProductApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        var productsEntity = await _productRepository.GetProductsAsync();
        return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        var productsEntity = await _productRepository.GetProductByIdAsync(id);
        return _mapper.Map<ProductDTO>(productsEntity);
    }
    
    public async Task AddProductAsync(ProductDTO productDTO)
    {
        var productsEntity = _mapper.Map<Product>(productDTO);
        await _productRepository.CreateProductAsync(productsEntity);
        productDTO.Id = productsEntity.Id;
    }

    public async Task UpdateProductAsync(ProductDTO productDTO)
    {
        var productsEntity = _mapper.Map<Product>(productDTO);
        await _productRepository.UpdateProductAsync(productsEntity);
    }

    public async Task RemoveProductAsync(int id)
    {
        var productsEntity = await _productRepository.GetProductByIdAsync(id);
        await _productRepository.DeleteProductAsync(productsEntity.Id);
    }
}
