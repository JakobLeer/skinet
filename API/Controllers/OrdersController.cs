using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService,
                                IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrderAsync(OrderDto orderDto)
        {
            string buyerEmail = HttpContext.User.GetEmail();

            var shipToAddress = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(buyerEmail,
                                                       orderDto.DeliveryMethodId,
                                                       orderDto.BasketId,
                                                       shipToAddress).ConfigureAwait(false);

            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating an order"));
            return Ok(order);
        }
    }
}