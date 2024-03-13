using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Ardalis.Specification;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ApplicationCore.Services.OrderServiceTests
{
    public class CreateOrder
    {
        private readonly IRepository<Basket> _mockBasketRepo = Substitute.For<IRepository<Basket>>();
        private readonly IRepository<BasketItem> _mockBasketItemRepo = Substitute.For<IRepository<BasketItem>>();
        private readonly IRepository<Product> _mockProductRepo = Substitute.For<IRepository<Product>>();
        private readonly IRepository<Order> _mockOrderRepo = Substitute.For<IRepository<Order>>();

        private readonly string _buyerId = "text-buyer-id";
        private readonly Address _testAddress = new Address() 
        { 
            City = "Istanbul",
            Country = "Turkey",
            Street = "Bagdat St.",
            ZipCode = "34728"
        };

        [Fact]
        public async Task ThrowsEmptyBasketExceptionWhenBasketIsEmpty()
        {
            var basketService = new BasketService(_mockBasketRepo, _mockBasketItemRepo, _mockProductRepo);
            var emptyBasket = new Basket() { Id = 1, BuyerId = _buyerId, Items= new() };
            _mockBasketRepo.FirstOrDefaultAsync(Arg.Any<ISpecification<Basket>>()).Returns(emptyBasket);
            var orderService = new OrderService(basketService, _mockOrderRepo);

            await Assert.ThrowsAsync<EmptyBasketException>(() => orderService.CreateOrderAsync(_buyerId, _testAddress));
        }
    }
}
