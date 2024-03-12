using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerId, Address shippingAddress);
    }
}
