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
    public class ShipmentService : IShipmentService
    {
        private readonly ShipmentDataStore _dataStore;

        public ShipmentService()
        {
            _dataStore = new ShipmentDataStore(); // lub użyj Dependency Injection
        }

        public Task<IEnumerable<Shipment>> GetAllAsync() => _dataStore.GetItemsAsync();
        public Task<Shipment> GetByIdAsync(int id) => _dataStore.GetItemAsync(id);
    }
}
