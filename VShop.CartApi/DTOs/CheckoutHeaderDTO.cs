namespace VShop.CartApi.DTOs;

public class CheckoutHeaderDTO
{
    public int Id { get; set; }
    public string UserId { get; set; } 
    public string CouponCode { get; set; } 

    public decimal TotalAmount { get; set; } = 0.00m;

    //desconto
    public decimal Discount { get; set; } = 0.00m;

    //checkout
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public DateTime DateTime { get; set; }
    public string Telephone { get; set; } 
    public string Email { get; set; } 
    public string CardNumber { get; set; } 
    public string NameOnCard { get; set; } 
    public string CVV { get; set; } 
    public string ExpireMothYear { get; set; }
    //
    public int CartTotalItens { get; set; }
    public IEnumerable<CartItemDTO> CartItems { get; set; }
}