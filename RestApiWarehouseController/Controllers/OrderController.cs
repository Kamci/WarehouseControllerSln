using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiWarehouseController.DTO;
using RestApiWarehouseController.Models;
using RestApiWarehouseController.Models.Contexts;

namespace RestApiWarehouseController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly WCContext _context;

        public OrderController(WCContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product) 
            .Select(o => new OrderDto
        {
            Id = o.Id,
            OrderDate = (DateTime)o.OrderDate,
            Status = o.Status,
            UserId = o.UserId,
            UserLogin = o.User.Login,
            OrderItems = o.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                OrderId = oi.OrderId,
                Quantity = oi.Quantity,
                ProductName = oi.Product.Name
            }).ToList()
        })
        .OrderByDescending(o => o.OrderDate)
        .ToListAsync();

            return orders;
        }
        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]

        public async Task<IActionResult> PutOrder(int id, Order updatedOrder)
        {
            if (id != updatedOrder.Id)
                return BadRequest();

            var existingOrder = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrder == null)
                return NotFound();

            existingOrder.OrderDate = updatedOrder.OrderDate;
            existingOrder.Status = updatedOrder.Status;
            existingOrder.UserId = updatedOrder.UserId;

            _context.OrderItems.RemoveRange(
                existingOrder.OrderItems.Where(oi =>
                    !updatedOrder.OrderItems.Any(uoi => uoi.Id == oi.Id))
            );

      
            foreach (var updatedItem in updatedOrder.OrderItems)
            {
                OrderItem existingItem;

                if (updatedItem.Id > 0)
                {
                 
                    existingItem = existingOrder.OrderItems.FirstOrDefault(oi => oi.Id == updatedItem.Id);
                }
                else
                {
                 
                    existingItem = existingOrder.OrderItems.FirstOrDefault(oi => oi.ProductId == updatedItem.ProductId);
                }

                if (existingItem != null)
                {
                
                    existingItem.ProductId = updatedItem.ProductId;
                    existingItem.Quantity = updatedItem.Quantity;
                }
                else
                {
              
                    existingOrder.OrderItems.Add(new OrderItem
                    {
                        ProductId = updatedItem.ProductId,
                        Quantity = updatedItem.Quantity,
                        OrderId = id
                    });
                }
            }


            await _context.SaveChangesAsync();

            return NoContent();
        }


        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

       
    }
}
