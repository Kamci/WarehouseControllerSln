using System.Collections.ObjectModel;
using WarehouseController.Model;
using WarehouseController.Services;

   public  class WarehouseDataStore : IDataStore<Warehouse>
    {
        private readonly ObservableCollection<Warehouse> warehouses = new()
    {
            new() { Id = 1, Name = "Warehouse 1", Location = "Location 1" },
            new() { Id = 2, Name = "Warehouse 2", Location = "Location 2" },
            new() { Id = 3, Name = "Warehouse 3", Location = "Location 3" },
    };


        public async Task<bool> AddItemAsync(Warehouse item)
        {
            warehouses.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Warehouse item)
        {
            var oldItem = warehouses.Where((Warehouse arg) => arg.Id == item.Id).FirstOrDefault();
            warehouses.Remove(oldItem);
            warehouses.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = warehouses.Where((Warehouse arg) => arg.Id == id).FirstOrDefault();
            warehouses.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<Warehouse> GetItemAsync(int id)
        {
            return await Task.FromResult(warehouses.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Warehouse>> GetItemsAsync(bool forceRefresh = false) => await Task.FromResult(warehouses);
    }
