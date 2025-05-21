using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;

namespace WarehouseController.Services
{
    public class SupplierDataStore : IDataStore<Supplier>
    {
        private readonly ObservableCollection<Supplier> suppliers = new()
        {
            new Supplier() { Id = 1, Name = "Supplier A", Contact = "Contact A" },
            new Supplier() { Id = 2, Name = "Supplier B", Contact = "Contact B" },
            new Supplier() { Id = 3, Name = "Supplier C", Contact = "Contact C" },
        };


        public async Task<bool> AddItemAsync(Supplier item)
        {
            suppliers.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Supplier item)
        {
            var oldItem = suppliers.Where((Supplier arg) => arg.Id == item.Id).FirstOrDefault();
            suppliers.Remove(oldItem);
            suppliers.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = suppliers.Where((Supplier arg) => arg.Id == id).FirstOrDefault();
            suppliers.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<Supplier> GetItemAsync(int id)
        {
            return await Task.FromResult(suppliers.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Supplier>> GetItemsAsync(bool forceRefresh = false) => await Task.FromResult(suppliers);
    }
}

