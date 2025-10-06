using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.Services.DataStores;
using WarehouseController.Services.Interfaces;

namespace WarehouseController.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryDataStore _dataStore;

        public CategoryService()
        {
            _dataStore = new CategoryDataStore(); 
        }

        public Task<IEnumerable<Category>> GetAllAsync() => _dataStore.GetItemsAsync();
        public Task<Category> GetByIdAsync(int id) => _dataStore.GetItemAsync(id);
    }
}
