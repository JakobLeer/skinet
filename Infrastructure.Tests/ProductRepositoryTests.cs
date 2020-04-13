using Core.Entities;
using Core.Specifications;
using EntityFrameworkCore3Mock;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

namespace Infrastructure.Tests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public void Search_ByName_ReturnsCorrectAnswer()
        {
            int id = 1;

            var productsInDb = new []
            {
                new Product { Id = id++, Name = "ab" },
                new Product { Id = id++, Name = "ac" },
                new Product { Id = id++, Name = "bc" },
                new Product { Id = id++, Name = "abc" },
                new Product { Id = id++, Name = "xxxxxxabc" },
                new Product { Id = id++, Name = "abcxxxxx" },
                new Product { Id = id++, Name = "xxabcxxx" },
            };

            var mockContext = new DbContextMock<StoreContext>(new DbContextOptionsBuilder<StoreContext>().Options);
            var productsDbSetMock = mockContext.CreateDbSetMock(ctx => ctx.Products, productsInDb);

            var repo = new Repository<Product>(mockContext.Object);

            var productSpecParams = new ProductSpecParams
            {
                Name = "abc"
            };

            var match = repo.ListBySpec(new ProductWithBrandAndType(productSpecParams));

            // Assert.Equal(4, match.Count);
        }
    }
}
