using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork,
                            IBasketRepository basketRepo)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
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
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(basketItem.Id).ConfigureAwait(false);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, basketItem.Quantity);
                orderItems.Add(orderItem);
            }

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId).ConfigureAwait(false);

            decimal subtotal = orderItems.Sum(oi => oi.Price * oi.Quantity);

            var order = new Order(orderItems, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            _unitOfWork.Repository<Order>().Add(order);

            int result = await _unitOfWork.CompleteAsync().ConfigureAwait(false);

            if (result <= 0) return null;

            await _basketRepo.DeleteBasketAsync(basketId).ConfigureAwait(false);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync().ConfigureAwait(false);
            return deliveryMethods;
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrdering(id, buyerEmail);

            var order = await _unitOfWork.Repository<Order>().GetBySpecAsync(spec).ConfigureAwait(false);

            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrdering(buyerEmail);

            var order = await _unitOfWork.Repository<Order>().ListBySpecAsync(spec).ConfigureAwait(false);

            return order;
        }
    }
}