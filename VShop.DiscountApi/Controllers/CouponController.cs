using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.DiscountApi.DTOs;
using VShop.DiscountApi.Repositories.Interface;

namespace VShop.DiscountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _repository;

        public CouponController(ICouponRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("{couponCode}")]
        [Authorize]
        public async Task<ActionResult<CouponDTO>> GetDiscountCouponByCode(string couponCode)
        {
            var coupon = await _repository.GetCouponByCodeAsync(couponCode);

            if (coupon == null)
                return NotFound($"Coupon Code: {couponCode} not found.");

            return Ok(coupon);
        }
    }
}
