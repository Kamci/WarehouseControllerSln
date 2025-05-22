using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.Services.Interfaces;

namespace WarehouseController.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly ProductDataStore _dataStore;

        public ProductService()
        {
            _dataStore = new ProductDataStore(); // lub użyj Dependency Injection
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _dataStore.GetItemsAsync();
        public Task<Product> GetByIdAsync(int id) => _dataStore.GetItemAsync(id);
    }
}
