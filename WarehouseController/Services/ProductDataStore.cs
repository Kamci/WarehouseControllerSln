using System.Collections.ObjectModel;
using WarehouseController.Model;
using WarehouseController.Services;
using WarehouseController.Services.Abstract;

public class ProductDataStore : AListDataStore<Product>
{
    public ProductDataStore() : base() { }

    protected override string ControllerName => "Product";
    protected override int GetId(Product item) => item.Id;
}
