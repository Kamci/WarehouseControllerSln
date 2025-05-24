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
    public class SupplierService : ISupplierService
    {
        private readonly SupplierDataStore _dataStore;

        public SupplierService()
        {
            _dataStore = new SupplierDataStore(); 
        }

        public Task<IEnumerable<Supplier>> GetAllAsync() => _dataStore.GetItemsAsync();
        public Task<Supplier> GetByIdAsync(int id) => _dataStore.GetItemAsync(id);
    }
}
