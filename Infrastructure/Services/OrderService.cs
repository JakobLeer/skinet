using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<DeliveryMethod> _deliveryMethodRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IBasketRepository _basketRepo;

        public OrderService(IRepository<Order> orderRepo,
                            IRepository<DeliveryMethod> deliveryMethodRepo,
                            IRepository<Product> productRepo,
                            IBasketRepository basketRepo)
        {
            _orderRepo = orderRepo;
            _deliveryMethodRepo = deliveryMethodRepo;
            _productRepo = productRepo;
            _basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync(
            string buyerEmail,
            int deliveryMethodId,
            string basketId,
            Address shippingAddress)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId).ConfigureAwait(false);

            // get items in basket and product prices from product DB
            var orderItems = new List<OrderItem>();

            foreach (var basketItem in basket.Items)
            {
                var productItem = await _productRepo.GetByIdAsync(basketItem.Id).ConfigureAwait(false);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, basketItem.Quantity);
                orderItems.Add(orderItem);
            }

            var deliveryMethod = await _deliveryMethodRepo.GetByIdAsync(deliveryMethodId).ConfigureAwait(false);

            decimal subtotal = orderItems.Sum(oi => oi.Price * oi.Quantity);

            var order = new Order(orderItems, buyerEmail, shippingAddress, deliveryMethod, subtotal);

            // TODO: save to db

            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Order> GetOrderById(int id, string buyerEmail)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new System.NotImplementedException();
        }
    }
}