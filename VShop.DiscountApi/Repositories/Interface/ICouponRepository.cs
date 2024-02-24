using VShop.DiscountApi.DTOs;

namespace VShop.DiscountApi.Repositories.Interface;

public interface ICouponRepository
{
    Task<CouponDTO> GetCouponByCodeAsync(string couponCode);
}
