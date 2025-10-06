using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseController.Model
{
    public class Shipment
    {
        public int Id { get; set; }

        public int? SupplierId { get; set; }
        public int? WarehouseId { get; set; }

        public DateTime ShipmentDate { get; set; }
        public string Status { get; set; }

        public Supplier? Supplier { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
