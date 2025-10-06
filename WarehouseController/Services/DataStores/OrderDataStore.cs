using System.Collections.ObjectModel;
using System.Net.Http.Json;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.Services.Abstract;

namespace WarehouseController.Services
{
    public class OrderDataStore : AListDataStore<OrderDto>
    {
        public OrderDataStore() : base() { }

        protected override string ControllerName => "Order";
        protected override int GetId(OrderDto item) => item.Id ?? 0;
   
    }
}
