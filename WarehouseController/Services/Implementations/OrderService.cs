using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.DTO;
using WarehouseController.Services.Interfaces;

namespace WarehouseController.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly OrderDataStore _dataStore;

        public OrderService()
        {
            _dataStore = new OrderDataStore(); // lub użyj Dependency Injection
        }

        public Task<IEnumerable<OrderDto>> GetAllAsync() => _dataStore.GetItemsAsync();
        public Task<OrderDto> GetByIdAsync(int id) => _dataStore.GetItemAsync(id);
    }
}
