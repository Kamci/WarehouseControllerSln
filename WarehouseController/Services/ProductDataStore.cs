using System.Collections.ObjectModel;
using WarehouseController.Model;
using WarehouseController.Services;

    public class ProductDataStore : IDataStore<Product>
{
    private readonly ObservableCollection<Product> products = new()
    {
        new() { Id = 1, Name = "Monitor", Price = 22.4m, Quantity = 1, CategoryId = 1, WarehouseId = 2 },
        new() { Id = 2, Name = "Monitor", Price = 22.4m, Quantity = 1, CategoryId = 1, WarehouseId = 2 },
        new() { Id = 3, Name = "Monitor", Price = 22.4m, Quantity = 1, CategoryId = 1, WarehouseId = 2 },
    };


    public async Task<bool> AddItemAsync(Product item)
    {
        products.Add(item);
        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateItemAsync(Product item)
    {
        var oldItem = products.Where((Product arg) => arg.Id == item.Id).FirstOrDefault();
        products.Remove(oldItem);
        products.Add(item);
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteItemAsync(int id)
    {
        var oldItem = products.Where((Product arg) => arg.Id == id).FirstOrDefault();
        products.Remove(oldItem);
        return await Task.FromResult(true);
    }

    public async Task<Product> GetItemAsync(int id)
    {
        return await Task.FromResult(products.FirstOrDefault(s => s.Id == id));
    }

    public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false) => await Task.FromResult(products);
}
