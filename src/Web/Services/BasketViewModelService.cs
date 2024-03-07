using ApplicationCore.Interfaces;
using System.Security.Claims;
using Web.Extensions;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private HttpContext HttpContext => _httpContextAccessor.HttpContext!;
        private string? UserId => HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        private string? AnonId => HttpContext.Request.Cookies[Constants.BASKET_COOKIE];

        private string BuyerId => UserId ?? AnonId ?? CreateAnonymousId();

        private string _createdAnonId = null!;

        // benzersiz bir geçici id oluştur, cookilerde depola, gizli bir field'a aktae ve döndür.
        private string CreateAnonymousId()
        {
            if (_createdAnonId != null) return _createdAnonId;

            _createdAnonId = Guid.NewGuid().ToString();
            HttpContext.Response.Cookies.Append(Constants.BASKET_COOKIE, _createdAnonId, new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(14),
                IsEssential = true // kullanıcının iznini önemsemeden cookide tutulmasını sağlar
            });

            return _createdAnonId;
        }

        public BasketViewModelService(IBasketService basketService, IHttpContextAccessor httpContextAccessor)
        {
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantity)
        {
            var basket = await _basketService.AddItemBasketAsync(BuyerId, productId, quantity);

            return basket.ToBasketViewModel();

        }
    }
}
