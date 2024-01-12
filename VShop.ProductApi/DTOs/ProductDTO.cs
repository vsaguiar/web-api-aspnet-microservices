using System.ComponentModel.DataAnnotations;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTOs;

public class ProductDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [MinLength(5)]
    [MaxLength(200)]
    public string Description { get; set; }

    [Required(ErrorMessage = "O estoque é obrigatório.")]
    [Range(1, 9999)]
    public long Stock { get; set; }
    public string ImageURL { get; set; }

    public Category Category { get; set; }
    public int CategoryId { get; set; }
}
