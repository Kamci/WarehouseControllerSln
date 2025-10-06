using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.Services.Abstract;
using WarehouseController.Services.Interfaces;

namespace WarehouseController.Services.Implementations
{
    public class WarehouseService : IWarehouseService
    {
        private readonly WarehouseDataStore _dataStore;

        public WarehouseService()
        {
            _dataStore = new WarehouseDataStore();
        }

        public Task<IEnumerable<Warehouse>> GetAllAsync() => _dataStore.GetItemsAsync();
        public Task<Warehouse> GetByIdAsync(int id) => _dataStore.GetItemAsync(id);
    }
}