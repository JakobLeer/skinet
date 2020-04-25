using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket basket)
        {
            var wasCreated = await _database.StringSetAsync(basket.Id,
                                                         JsonSerializer.Serialize<CustomerBasket>(basket),
                                                         TimeSpan.FromDays(30));
            
            if (!wasCreated) return null;

            var newBasket = await GetBasketAsync(basket.Id);

            return newBasket;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            var wasDeleted = await _database.KeyDeleteAsync(basketId);
            return wasDeleted;
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var basketRepresentation = await _database.StringGetAsync(basketId);
            var customerBasket = basketRepresentation.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basketRepresentation);
            return customerBasket;
        }
    }
}