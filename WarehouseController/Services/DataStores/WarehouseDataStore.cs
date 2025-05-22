using System.Collections.ObjectModel;
using WarehouseController.Model;
using WarehouseController.Services;
using WarehouseController.Services.Abstract;

public  class WarehouseDataStore : AListDataStore<Warehouse>
{
    public WarehouseDataStore() : base() { }

    protected override string ControllerName => "Warehouse";
    protected override int GetId(Warehouse item) => item.Id;
}
