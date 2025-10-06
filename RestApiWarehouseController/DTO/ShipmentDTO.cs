using RestApiWarehouseController.Models;

namespace RestApiWarehouseController.DTO
{
    public class ShipmentDTO
    {
        public int Id { get; set; }

        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public int? WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public string Status { get; set; }

        public Supplier? Supplier { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
