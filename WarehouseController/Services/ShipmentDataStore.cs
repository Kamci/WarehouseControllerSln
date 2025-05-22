using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WarehouseController.Model;
using WarehouseController.Services;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseController.Services
{
    public class ShipmentDataStore : IDataStore<Shipment>
    {
        private readonly ObservableCollection<Shipment> shipments = new()
    {
        new() { Id = 1, SupplierId = 1, WarehouseId = 1, ShipmentDate = DateTime.Now, Status = "Pending" },
        new() { Id = 2, SupplierId = 2, WarehouseId = 2, ShipmentDate = DateTime.Now.AddDays(-1), Status = "Completed" },
        new() { Id = 3, SupplierId = 3, WarehouseId = 3, ShipmentDate = DateTime.Now.AddDays(-2), Status = "In Transit" },
    };


        public async Task<bool> AddItemAsync(Shipment item)
        {
            shipments.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Shipment item)
        {
            var oldItem = shipments.Where((Shipment arg) => arg.Id == item.Id).FirstOrDefault();
            shipments.Remove(oldItem);
            shipments.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = shipments.Where((Shipment arg) => arg.Id == id).FirstOrDefault();
            shipments.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<Shipment> GetItemAsync(int id)
        {
            return await Task.FromResult(shipments.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Shipment>> GetItemsAsync(bool forceRefresh = false) => await Task.FromResult(shipments);
    }
}