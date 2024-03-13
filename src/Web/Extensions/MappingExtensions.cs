using ApplicationCore.Entities;
using Web.Models;

namespace Web.Extensions
{
    public static class MappingExtensions
    {
        public static BasketViewModel ToBasketViewModel(this Basket basket) // this ile ekledik web katmanında basket. metodlarında göreceğiz.
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
                    ProductName = x.Product.Name,
                    PictureUri = x.Product.PictureUri ?? "noimage.jpg",
                    UnitPrice = x.Product.Price
                }).ToList()
            };
        }
    }
    
}
