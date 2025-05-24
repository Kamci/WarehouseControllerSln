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
        public int? Id { get; set; } 
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public string UserLogin { get; set; } = string.Empty;
        public List<OrderItemDto> OrderItems { get; set; }

        [JsonIgnore]
        public string DisplayName => $"Order #{Id}";

        [JsonIgnore]
        public string DisplayDate => OrderDate.ToString("yyyy-MM-dd");

        [JsonIgnore]
        public int ProductCount => OrderItems?.Sum(i => i.Quantity) ?? 0;
    }
}
