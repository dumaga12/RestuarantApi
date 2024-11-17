using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public OrdersController(RestaurantContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            foreach (var itemId in order.OrderItems.Select(oi => oi.MenuItemId))
            {
                if (!_context.MenuItems.Any(m => m.Id == itemId))
                    return NotFound($"MenuItem with ID {itemId} not found.");
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllOrders), new { id = order.Id }, order);
        }
    }
}
