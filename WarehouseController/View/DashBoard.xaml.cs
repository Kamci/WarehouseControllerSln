using WarehouseController.ViewModel.Dashboard;

namespace WarehouseController.View;

public partial class DashBoard : ContentPage
{
    private readonly DashboardViewModel _vm;

    public DashBoard(DashboardViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_vm.Warehouses.Count == 0)
            await _vm.LoadWarehousesAsync();
    }
}