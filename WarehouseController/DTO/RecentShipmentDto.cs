using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;

namespace WarehouseController.DTO
{
    public class RecentShipmentDto
    {
        private ShipmentDTO s;

        public string ShipmentDate { get; set; }
        public string SupplierName { get; set; }
        public string WarehouseName { get; set; }

        public RecentShipmentDto(ShipmentDTO s, string supplierName, string warehouseName)
        {
            ShipmentDate = s.ShipmentDate?.ToString("yyyy-MM-dd");
            SupplierName = supplierName;
            WarehouseName = warehouseName;
        }

      
    }
}
