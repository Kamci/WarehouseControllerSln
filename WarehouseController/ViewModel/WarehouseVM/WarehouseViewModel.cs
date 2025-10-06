using WarehouseController.Model;
using WarehouseController.ViewModel.Abstract;
using WarehouseController.View.WarehouseView;
namespace WarehouseController.ViewModel.WarehouseVM
{
    class WarehouseViewModel:  AItemListViewModel<Warehouse>
    {
        private Warehouse? _selectedWarehouse;
    public Command RefreshCommand { get; }
    public Command<Warehouse> AboutCommand { get; }
    public WarehouseViewModel() : base("Warehouse")
    {
        RefreshCommand = LoadItemsCommand;
        AboutCommand = new Command<Warehouse>(OnWarehouseSelected);
    }



    public Warehouse? SelectedWarehouse
        {
            get => _selectedWarehouse;
            set => SetProperty(ref _selectedWarehouse, value);
        }
    
    
    async void OnWarehouseSelected(Warehouse warehouse)
    {
        await GoToDetailsPage(warehouse);
    }

    public override async Task GoToAddPage()
    {
            
            await Shell.Current.GoToAsync(nameof(AddWarehousePage));
        }

    public override async Task GoToDetailsPage(Warehouse warehouse)
    {
        if (warehouse == null)
            return;
        SelectedWarehouse = warehouse;
       
        await Shell.Current.GoToAsync($"{nameof(DetailWarehousePage)}?{nameof(DetailWarehouseViewModel.ItemId)}={warehouse.Id}");
    }
}
}
    