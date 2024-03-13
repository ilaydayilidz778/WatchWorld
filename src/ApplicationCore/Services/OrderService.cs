using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IBasketService basketService, IRepository<Order> orderRepository)
        {
            _basketService = basketService;
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(string buyerId, Address shippingAddress)
        {
           var basket = await _basketService.GetOrCreateBasketAsync(buyerId);

           if(basket.Items.Count == 0)
            {
                throw new EmptyBasketException();
            }

            Order order = new Order()
            {
                ShipToAddress = shippingAddress,
                BuyerId = buyerId,
                Items = basket.Items.Select(x => new OrderItem()
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    ProductName = x.Product.Name,
                    UnitPrice = x.Product.Price,
                    PictureUri = x.Product.PictureUri,

                }).ToList()
            };

            return await _orderRepository.AddAsync(order);
        }
    }
}
