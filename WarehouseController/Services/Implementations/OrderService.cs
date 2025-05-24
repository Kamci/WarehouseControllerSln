using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.Services.Interfaces;

namespace WarehouseController.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly OrderDataStore _dataStore;

        public OrderService()
        {
            _dataStore = new OrderDataStore();
        }

        public Task<IEnumerable<OrderDto>> GetAllAsync() => _dataStore.GetItemsAsync();
        public Task<OrderDto> GetByIdAsync(int id) => _dataStore.GetItemAsync(id);

        public async Task<List<OrderDto>> GetRecentOrdersAsync(int warehouseId)
        {
            var allOrders = await GetAllAsync();
            var allProducts = await new ProductService().GetAllAsync();

            var productIdsFromWarehouse = allProducts
                .Where(p => p.WarehouseId == warehouseId)
                .Select(p => p.Id)
                .ToHashSet();

            var filteredOrders = allOrders
                .Where(order => order.OrderItems != null &&
                                order.OrderItems.Any(item => productIdsFromWarehouse.Contains(item.ProductId)))
                .OrderByDescending(order => order.OrderDate)
                .Take(5)
                .ToList();
          
            return filteredOrders;
        }
        public async Task<int> GetOpenOrdersCountAsync(int warehouseId)
        {
            var allOrders = await _dataStore.GetItemsAsync();
            var allProducts = await new ProductService().GetAllAsync();

            var productIdsFromWarehouse = allProducts
                .Where(p => p.WarehouseId == warehouseId)
                .Select(p => p.Id)
                .ToHashSet();

            var openOrdersCount = allOrders
                .Where(order => order.Status == "Open" &&
                                order.OrderItems.Any(item => productIdsFromWarehouse.Contains(item.ProductId)))
                .Count();

            return openOrdersCount;
        }
    }
}
