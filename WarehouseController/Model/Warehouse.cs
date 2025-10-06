using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseController.Model
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
    }
}
