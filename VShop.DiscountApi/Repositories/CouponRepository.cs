using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VShop.DiscountApi.Context;
using VShop.DiscountApi.DTOs;
using VShop.DiscountApi.Repositories.Interface;

namespace VShop.DiscountApi.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CouponRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CouponDTO> GetCouponByCodeAsync(string couponCode)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
        return _mapper.Map<CouponDTO>(coupon);
    }
}
