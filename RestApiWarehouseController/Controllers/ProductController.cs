using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiWarehouseController.DTO;
using RestApiWarehouseController.Models;
using RestApiWarehouseController.Models.Contexts;

namespace RestApiWarehouseController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly WCContext _context;

        public ProductController(WCContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products
               .Include(p => p.Warehouse)
               .Include(p => p.Supplier)
               .Include(p => p.Category)
               .OrderByDescending(p => p.Id)
               .ToListAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                WarehouseName = p.Warehouse?.Name ?? "",
                SupplierName = p.Supplier?.Name ?? "",
                CategoryName = p.Category?.Name ?? "",
                WarehouseId = p.WarehouseId,
                SupplierId = p.SupplierId,
                CategoryId = p.CategoryId
            }).ToList();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products
        .Include(p => p.Warehouse)
        .Include(p => p.Supplier)
        .Include(p => p.Category)
        .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                WarehouseId = product.WarehouseId,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                WarehouseName = product.Warehouse?.Name ?? "",
                SupplierName = product.Supplier?.Name ?? "",
                CategoryName = product.Category?.Name ?? ""
            };
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            // przypisanie danych z DTO
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.WarehouseId = dto.WarehouseId;
            product.SupplierId = dto.SupplierId;
            product.CategoryId = dto.CategoryId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                WarehouseId = dto.WarehouseId,
                SupplierId = dto.SupplierId,
                CategoryId = dto.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            dto.Id = product.Id; // zwróć ID
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, dto);
        }
        [Authorize(Roles = "Admin")]
        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
