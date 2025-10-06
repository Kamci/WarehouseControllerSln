using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WarehouseController.Model;

namespace WarehouseController.DTO
{
     public class ProductDto
    {

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public string WarehouseName { get; set; } = "";
        public string SupplierName { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public int? WarehouseId { get;  set; }
        public int? CategoryId { get;  set; }
        public int? SupplierId { get;  set; }


    }
}
