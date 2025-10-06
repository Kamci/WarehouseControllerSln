using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.Services.Interfaces;

namespace WarehouseController.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly ProductDataStore _dataStore;

        public ProductService()
        {
            _dataStore = new ProductDataStore();
        }

        public Task<IEnumerable<ProductDto>> GetAllAsync() => _dataStore.GetItemsAsync();
        public Task<ProductDto> GetByIdAsync(int id) => _dataStore.GetItemAsync(id);

        public async Task<List<ProductDto>> GetTopProductsAsync(int warehouseId)
        {
            var all = await GetAllAsync();
            return all.Where(p => p.WarehouseId == warehouseId)
                      .OrderByDescending(p => p.StockQuantity)
                      .Take(5)
                      .ToList();
        }

        public async Task<int> GetProductsCountAsync(int warehouseId)
                => (await GetAllAsync())
                .Count(p => p.WarehouseId == warehouseId);

        public async Task<int> GetLowStockItemCountAsync(int warehouseId)
        {
            var allProducts = await GetAllAsync();
            return allProducts
                .Where(p => p.WarehouseId == warehouseId && p.StockQuantity < 5)
                .Count();
        }

      
    }
}
