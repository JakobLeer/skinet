using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBasketRepository, BasketRepository>();

            services.Configure<ApiBehaviorOptions>(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var errors = actionContext.ModelState
                                        .Where (m => m.Value.Errors.Count > 0)
                                        .SelectMany(m => m.Value.Errors)
                                        .Select(e => e.ErrorMessage).ToArray();

                        var validationError = new ApiValidationError()
                        {
                            Errors = errors
                        };

                        return new BadRequestObjectResult(validationError);
                    };
                }
            );

            return services;
        }
    }
}