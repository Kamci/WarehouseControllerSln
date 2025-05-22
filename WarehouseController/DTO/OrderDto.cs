using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WarehouseController.DTO
{
    public class OrderDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Id { get; set; } // <- może być null przy dodawaniu nowego zamówienia
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
