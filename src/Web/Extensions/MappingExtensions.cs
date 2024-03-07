using ApplicationCore.Entities;
using Web.Models;

namespace Web.Extensions
{
    public static class MappingExtensions
    {
        public static BasketViewModel ToBasketViewModel(this Basket basket)
        {
            return new BasketViewModel()
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(x => new BasketItemViewModel()
                {
                    Id = x.Id,
                    Quantity = x.Quantity,
                    ProductId = x.ProductId,
                    PictureUri = x.Product.PictureUri ?? "noimage.jpg",
                    UnitPrice = x.Product.Price
                }).ToList(),
            };
        }
    }
    }
}
