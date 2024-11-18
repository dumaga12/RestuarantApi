using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Data; 
using RestaurantAPI.Models; 
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public MenuItemsController(RestaurantContext context)
        {
            _context = context;
        }

        // POST: api/MenuItems
        [HttpPost]
        public async Task<ActionResult<MenuItem>> PostMenuItem(MenuItem menuItem)
        {
            // Add the new menu item to the database
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            // Return the newly created item
            return CreatedAtAction("GetMenuItem", new { id = menuItem.Id }, menuItem);
        }
    }
}
