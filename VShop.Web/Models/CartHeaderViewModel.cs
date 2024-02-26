namespace VShop.Web.Models;

public class CartHeaderViewModel
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string CouponCode { get; set; }

    public decimal TotalAmount { get; set; } = 0.00m;

    // Desconto
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
    public string ExpireMonthYear { get; set; } 
}
