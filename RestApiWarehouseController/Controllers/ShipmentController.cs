using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiWarehouseController.DTO;
using RestApiWarehouseController.Models;
using RestApiWarehouseController.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiWarehouseController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly WCContext _context;

        public ShipmentController(WCContext context)
        {
            _context = context;
        }

        // GET: api/Shipment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> GetShipments()
        {
            var shipments = await _context.Shipments
                .Include(s => s.Supplier)
                .Include(s => s.Warehouse)
                .OrderByDescending(s => s.ShipmentDate)
                .ToListAsync();

            var shipmentDTOs = shipments.Select(s => new ShipmentDTO
            {
                Id = s.Id,
                SupplierId = s.SupplierId,
                SupplierName = s.Supplier?.Name ?? "",
                WarehouseId = s.WarehouseId,
                WarehouseName = s.Warehouse?.Name ?? "",
                ShipmentDate = s.ShipmentDate,
                Status = s.Status
            }).ToList();

            return Ok(shipmentDTOs);
        }

        // GET: api/Shipment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipmentDTO>> GetShipment(int id)
        {
            var shipment = await _context.Shipments
                .Include(s => s.Supplier)
                .Include(s => s.Warehouse)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (shipment == null)
                return NotFound();

            var dto = new ShipmentDTO
            {
                Id = shipment.Id,
                SupplierId = shipment.SupplierId,
                SupplierName = shipment.Supplier?.Name ?? "",
                WarehouseId = shipment.WarehouseId,
                WarehouseName = shipment.Warehouse?.Name ?? "",
                ShipmentDate = shipment.ShipmentDate,
                Status = shipment.Status
            };

            return Ok(dto);
        }

        // POST: api/Shipment
        [HttpPost]
        public async Task<ActionResult<ShipmentDTO>> PostShipment(ShipmentDTO dto)
        {
            var shipment = new Shipment
            {
                SupplierId = dto.SupplierId,
                WarehouseId = dto.WarehouseId,
                ShipmentDate = dto.ShipmentDate,
                Status = dto.Status
            };

            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();

            dto.Id = shipment.Id; 
            return CreatedAtAction(nameof(GetShipment), new { id = shipment.Id }, dto);
        }

        // PUT: api/Shipment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipment(int id, ShipmentDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
                return NotFound();

            shipment.SupplierId = dto.SupplierId;
            shipment.WarehouseId = dto.WarehouseId;
            shipment.ShipmentDate = dto.ShipmentDate;
            shipment.Status = dto.Status;

            _context.Entry(shipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ShipmentExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        // DELETE: api/Shipment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
                return NotFound();

            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShipmentExists(int id)
        {
            return _context.Shipments.Any(e => e.Id == id);
        }
    }
}